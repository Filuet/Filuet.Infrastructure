using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    /// <summary>
    /// Shopping cart
    /// </summary>
    public class Cart {
        [JsonPropertyName("items")]
        public IEnumerable<CartItem> Items { get; set; }

        /// <summary>
        /// Additional parameters that take participation in calculation process
        /// </summary>
        /// <example>HLF: kiosk mode, month (in case of dual month period), consumption type...</example>
        [JsonPropertyName("additionalParams")]
        public Dictionary<string, string> AdditionalParams { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("shipping")]
        public ShippingDetails ShippingDetails { get; set; }

        public CartItem this[string sku]
          => Items.FirstOrDefault(x => x.Sku == sku.ToFineSku());

        [JsonIgnore]
        public bool IsEmpty
            => Items == null || !Items.Any() || !Items.Any(x => x.Quantity > 0);

        /// <summary>
        /// The cart contains only one product
        /// </summary>
        [JsonIgnore]
        public bool IsOneProductCart
            => Items != null && Items.Count() == 1;

        public static Cart Create(IEnumerable<CartItem> items, Dictionary<string, string> additionalParams = null)
            => new Cart { Items = items, AdditionalParams = additionalParams };

        public string GetParam(string key)
           => AdditionalParams.ContainsKey(key) ? AdditionalParams[key] : null;

        public Cart AddParam(string key, object value) {
            AdditionalParams.TryAdd(key, value.ToString());
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

        public static Cart operator +(Cart cart, CartItem item) {
            Func<CartItem, bool> p = x => string.Equals(x.Sku, item.Sku.ToFineSku(), StringComparison.InvariantCultureIgnoreCase);
            if (cart.Items.Any(p))
                cart.Items.FirstOrDefault(p).Quantity += item.Quantity;
            else {
                var newCartItems = cart.Items.ToList();
                newCartItems.Add(item);
                cart.Items = newCartItems;
            }

            return cart;
        }

        public static Cart operator -(Cart cart, CartItem item) {
            Func<CartItem, bool> p = x => string.Equals(x.Sku, item.Sku.ToFineSku(), StringComparison.InvariantCultureIgnoreCase);
            Func<CartItem, bool> rp = x => !string.Equals(x.Sku, item.Sku.ToFineSku(), StringComparison.InvariantCultureIgnoreCase);

            if (cart.Items.Any(p)) {
                if (Math.Max(0, cart.Items.FirstOrDefault(p).Quantity - item.Quantity) == 0)
                    cart.Items = cart.Items.Where(rp);
                else cart.Items.FirstOrDefault(p).Quantity = Math.Max(0, cart.Items.FirstOrDefault(p).Quantity - item.Quantity);
            }

            return cart;
        }

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

        [JsonIgnore]
        public IEnumerable<string> Skus
            => Items.Select(x => x.Sku).Distinct().OrderBy(x => x); // Distinct is here JIC

        public void Exclude(IEnumerable<string> skus) {
            if (skus == null || !skus.Any())
                return;

            Items = Items.Where(x => !skus.Contains(x.Sku?.Trim()));
        }

        public Cart Modify(Action<Cart> setupCart) {
            setupCart(this);
            return this;
        }

        public void Clear() {
            Items = new List<CartItem>();
        }
    }
}