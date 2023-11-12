using PotterKata.Abstracts;
using PotterKata.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterKata.Models
{
    public class ShoppingCart {

		public Dictionary<BookID, int> books;
		private PriceCalculator priceCalculatorComponent;
		public ShoppingCart (PriceCalculatorType type) {
			books = new Dictionary<BookID, int>();
			AddPriceCalculator(type);
		}

		public void AddBookToCart(BookID book, int quantity = 1) {
			if (books.ContainsKey(book)) {
				books[book] += quantity;
			}
			else {
				books.Add(book, quantity);
			}
		}

		public void RemoveBookFromCart (BookID book, int? quantity = null) {
			if (!books.ContainsKey(book)) {
				return;
			}

			if (!quantity.HasValue) {
				books.Remove(book);
				return;
			}

			books[book] -= quantity.GetValueOrDefault();
			if (books[book] <= 0) {
				books.Remove(book);
			}
		}
		public void ClearCart() {
			books.Clear(); 
		}

		public void AddPriceCalculator(PriceCalculatorType type) {
			switch(type) {
				case PriceCalculatorType.PotterDiscount:
					priceCalculatorComponent = new PotterDiscountPriceCalculator();
					break;
				default:
					priceCalculatorComponent = new PriceCalculator();
					break;
			}
		}

		public GroupingSolution Submit () {

			return priceCalculatorComponent.CalculatePrice(books);

		}


	}
}
