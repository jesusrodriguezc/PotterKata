using PotterKata.Data;
using PotterKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterKata.Abstracts
{
    public enum PriceCalculatorType {
        None,
        PotterDiscount,

    }
    public class PriceCalculator
    {
        public virtual GroupingSolution CalculatePrice (Dictionary<BookID, int> books) {
            
            GroupingSolution groupingSolution = new GroupingSolution ();
            groupingSolution.totalPrice = books.Sum(book => Book.BookData[book.Key].Price);
            groupingSolution.groupsOfBooks = books.GroupBy(book => book.Key).ToDictionary(book => new BookDiscountGroup( book.Key.ToString() ), book => book.Count());
            return groupingSolution;
		}

        
    }
}
