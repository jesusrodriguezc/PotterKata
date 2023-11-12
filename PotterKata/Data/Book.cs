using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterKata.Data {
	public enum BookID {
		FIRST_POTTER_BOOK ,
		SECOND_POTTER_BOOK ,
		THIRD_POTTER_BOOK,
		FOURTH_POTTER_BOOK,
		FIFTH_POTTER_BOOK,
		A_GAME_OF_THRONES
	}

	public enum BookSerieID {
		HARRY_POTTER,
		SONGS_OF_ICE_AND_FIRE
	}
	public class Book {
		public BookID Id { get; set; }
		public BookSerieID Serie { get; set; }
		public double Price { get; set; }


		// It should go into a Book table in a Database.
		public static readonly Dictionary<BookID, Book> BookData = new Dictionary<BookID, Book>()
		{
			{ BookID.FIRST_POTTER_BOOK, new Book(){ Id = BookID.FIRST_POTTER_BOOK, Serie = BookSerieID.HARRY_POTTER, Price = 8f} },
			{ BookID.SECOND_POTTER_BOOK, new Book(){ Id = BookID.SECOND_POTTER_BOOK, Serie = BookSerieID.HARRY_POTTER, Price = 8f} },
			{ BookID.THIRD_POTTER_BOOK, new Book(){ Id = BookID.THIRD_POTTER_BOOK, Serie = BookSerieID.HARRY_POTTER, Price = 8f}},
			{ BookID.FOURTH_POTTER_BOOK, new Book () { Id = BookID.FOURTH_POTTER_BOOK, Serie = BookSerieID.HARRY_POTTER, Price = 8f} },
			{ BookID.FIFTH_POTTER_BOOK, new Book () { Id = BookID.FIFTH_POTTER_BOOK, Serie = BookSerieID.HARRY_POTTER, Price = 8f} },
			{ BookID.A_GAME_OF_THRONES, new Book () { Id = BookID.A_GAME_OF_THRONES, Serie = BookSerieID.SONGS_OF_ICE_AND_FIRE, Price = 10f} },

		};
    }
}
