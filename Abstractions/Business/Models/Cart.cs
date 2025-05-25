using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    /// <summary>
    /// Shopping cart
    /// </summary>
    public class Cart
    {
        [JsonPropertyName("items")]
        public IEnumerable<CartItem> Items { get; set; }

        /// <summary>
        /// Additional parameters that take participation in calculation process
        /// </summary>
        /// <example>HLF: kiosk mode, month (in case of dual month period), consumption type...</example>
        [JsonPropertyName("additionalParams")]
        public Dictionary<string, object> AdditionalParams { get; set; } = new Dictionary<string, object>();

        public static Cart Create(IEnumerable<CartItem> items, Dictionary<string, object> additionalParams = null)
            => new Cart { Items = items, AdditionalParams = additionalParams };

        public T GetParam<T>(string key)
           => AdditionalParams.ContainsKey(key) ? (T)AdditionalParams[key] : default;

        public Cart AddParam(string key, object value) {
            AdditionalParams.TryAdd(key, value);
            return this;
        }

        public override bool Equals(object obj) {
            if (!(obj is Cart))
                return false;

            Cart cart = (Cart)obj;

            if (Items.Count() != cart.Items.Count())
                return false;

            if (Items.Any(x => !cart.Items.Any(y => x.Sku == y.Sku && x.Quantity == y.Quantity)))
                return false;

            return true;
        }

        public override int GetHashCode() => (Items.OrderBy(x => x.Sku).Select(x => (x.Sku.GetHashCode(), x.Quantity)).GetHashCode());

        public static bool operator ==(Cart lhs, Cart rhs) => lhs.Equals(rhs);

        public static bool operator !=(Cart lhs, Cart rhs) => !(lhs == rhs);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">minuend</param>
        /// <param name="b">subtrahend</param>
        /// <returns>In order to make A==B, you must add @missing to @B and remove @redundant from @A</returns>
        public static (IEnumerable<CartItem> missing, IEnumerable<CartItem> redundant) operator -(Cart a, Cart b) {
            if (a == null || b == null)
                throw new NullReferenceException();

            List<CartItem> toAdd = new List<CartItem>();

            if (a.Items != null) {
                toAdd.AddRange(a.Items.Where(x => b.Items == null || !b.Items.Any(y => y.Sku == x.Sku))); // totally new skus

                if (b.Items != null) {
                    IEnumerable<CartItem> deltaAdd = a.Items.Where(x => b.Items.Any(y => y.Sku == x.Sku && y.Quantity < x.Quantity)); // add skus that exist but quantity is less
                    toAdd.AddRange(deltaAdd.Select(x => CartItem.Create(x.Sku, (x.Quantity - (b.Items?.First(y => y.Sku == x.Sku).Quantity ?? 0)))));
                }
            }

            List<CartItem> toRemove = new List<CartItem>();

            if (b.Items != null) {
                toRemove.AddRange(b.Items.Where(x => a.Items == null || !a.Items.Any(y => y.Sku == x.Sku))); // totally redundant skus

                if (a.Items != null) {
                    IEnumerable<CartItem> deltaRemove = b.Items.Where(x => a.Items.Any(y => y.Sku == x.Sku && y.Quantity < x.Quantity)); // remove skus that exist but quantity is more
                    toRemove.AddRange(deltaRemove.Select(x => CartItem.Create(x.Sku, (x.Quantity - (a.Items?.First(y => y.Sku == x.Sku).Quantity ?? 0)))));
                }
            }

            return (toAdd, toRemove);
        }
    }
}