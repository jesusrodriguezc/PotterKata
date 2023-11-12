using PotterKata.Abstracts;
using PotterKata.Models;

namespace PotterKataTestingProject {
	public class ShoppingCart_SubmitTester {
		[Fact]
		public void TestWithoutDiscount () {
			ShoppingCart shoppingCart = new ShoppingCart (PriceCalculatorType.PotterDiscount);

			Assert.Equal(0f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIRST_POTTER_BOOK);
			Assert.Equal(8f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.ClearCart();
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK);
			Assert.Equal(8f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.ClearCart();
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.THIRD_POTTER_BOOK);
			Assert.Equal(8f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.ClearCart();
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FOURTH_POTTER_BOOK);
			Assert.Equal(8f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.ClearCart();
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK);
			Assert.Equal(8f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK);
			Assert.Equal(8 * 3, shoppingCart.Submit().totalPrice, 2);
		}

		[Fact]
		public void TestSimpleDiscounts () {
			ShoppingCart shoppingCart = new ShoppingCart(PriceCalculatorType.PotterDiscount);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIRST_POTTER_BOOK);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK);
			Assert.Equal(8*2*0.95f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.THIRD_POTTER_BOOK);
			Assert.Equal(8*3*0.9f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FOURTH_POTTER_BOOK);
			Assert.Equal(8*4*0.8f, shoppingCart.Submit().totalPrice, 2);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK);
			Assert.Equal(8*5*0.75f, shoppingCart.Submit().totalPrice, 2);
		}

		[Fact]
		public void TestMultipleDiscounts () {
			ShoppingCart shoppingCart = new ShoppingCart(PriceCalculatorType.PotterDiscount);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIRST_POTTER_BOOK, 2);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK);
			Assert.Equal(8 + (8 * 2 * 0.95f), shoppingCart.Submit().totalPrice, 2);	// [0 0 1]

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK);
			Assert.Equal(2 * 8 * 2 * 0.95f, shoppingCart.Submit().totalPrice, 2);	// [ 0 0 1 1 ]

			shoppingCart.RemoveBookFromCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK, 1);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.THIRD_POTTER_BOOK, 2);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FOURTH_POTTER_BOOK);
			Assert.Equal((8 * 4 * 0.8f) + (8 * 2 * 0.95f), shoppingCart.Submit().totalPrice, 2);	// [ 0 0 1 2 2 3 ]

			shoppingCart.RemoveBookFromCart(PotterKata.Data.BookID.FIRST_POTTER_BOOK, 1);
			shoppingCart.RemoveBookFromCart(PotterKata.Data.BookID.THIRD_POTTER_BOOK, 1);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK, 1);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK);

			Assert.Equal(8 + (8 * 5 * 0.75f), shoppingCart.Submit().totalPrice, 2);    // [0, 1, 1, 2, 3, 4]
		}

		[Fact]
		public void TestEdgeCases () {
			ShoppingCart shoppingCart = new ShoppingCart(PriceCalculatorType.PotterDiscount);

			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIRST_POTTER_BOOK, 2);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK, 2);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.THIRD_POTTER_BOOK, 2);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FOURTH_POTTER_BOOK);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK);
			Assert.Equal(2 * (8 * 4 * 0.8f), shoppingCart.Submit().totalPrice, 2);    // [0, 0 1, 1, 2, 2 3, 4]

			shoppingCart.ClearCart();
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIRST_POTTER_BOOK, 5);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK, 5);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.THIRD_POTTER_BOOK, 4);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FOURTH_POTTER_BOOK, 5);
			shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK, 4);
			Assert.Equal(3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8), shoppingCart.Submit().totalPrice, 2);    // 5 0's, 5 1's, 4 2's, 5 3's and 4 4's
		}
	}
}