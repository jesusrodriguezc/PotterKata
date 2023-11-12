using PotterKata.Abstracts;
using PotterKata.Models;

internal class Program {
	private static void Main (string[] args) {

		ShoppingCart shoppingCart = new ShoppingCart(PriceCalculatorType.PotterDiscount);

		shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIRST_POTTER_BOOK, 5);
		shoppingCart.AddBookToCart(PotterKata.Data.BookID.SECOND_POTTER_BOOK, 5);
		shoppingCart.AddBookToCart(PotterKata.Data.BookID.THIRD_POTTER_BOOK, 4);
		shoppingCart.AddBookToCart(PotterKata.Data.BookID.FOURTH_POTTER_BOOK, 5);
		shoppingCart.AddBookToCart(PotterKata.Data.BookID.FIFTH_POTTER_BOOK, 4);

		var solution = shoppingCart.Submit();

		Console.WriteLine(solution.ToString());


	}
}