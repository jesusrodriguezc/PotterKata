using PotterKata.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterKata.Models {
	public class BookDiscountGroup : List<BookID> {

		public BookDiscountGroup(string groupOfBooksStr) : base()
		{
			foreach(var idBookStr in groupOfBooksStr.Split(",")) {
				if (!Enum.TryParse(idBookStr, true, out BookID currentBookID)) {
					throw new Exception($"El ID {idBookStr} no corresponde a ningún libro.");
				}
				Add(currentBookID);

			}
		}

		public BookDiscountGroup (IEnumerable<BookID> collection) : base(collection) {
		}

		public override string ToString () {
			return string.Join(",", this.OrderBy(key => key));
		}

		#region Dictionary Access
		public override bool Equals (object? obj) {
			if (obj == null) return false;
			return Equals(obj as BookDiscountGroup);
		}

		public bool Equals(BookDiscountGroup group) {
			return ToString().Equals(group.ToString());
		}
		public override int GetHashCode () {
			return ToString().GetHashCode();
		}
		#endregion
	}
	public class GroupingSolution {
		public Dictionary<BookDiscountGroup, int> groupsOfBooks;
		public double totalPrice;

		public override string ToString () {
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Discount book packs");
			stringBuilder.AppendLine("--------------------------------------------");

			foreach (var groupOfBooks in groupsOfBooks) {
				for(int i = 0; i < groupOfBooks.Value; i++) {
					stringBuilder.AppendLine(groupOfBooks.Key.ToString());
				}
			}

			var priceBeforeDiscount = groupsOfBooks.Sum(group => group.Key.Count * group.Value * 8f);
			var discountAmount = totalPrice - priceBeforeDiscount;
			stringBuilder.AppendLine("--------------------------------------------");
			stringBuilder.AppendLine($"Price before discount: {Math.Round(priceBeforeDiscount, 2)} euros");
			stringBuilder.AppendLine("--------------------------------------------");
			stringBuilder.AppendLine($"Total discount: {Math.Round(discountAmount, 2)} euros");
			stringBuilder.AppendLine("--------------------------------------------");
			stringBuilder.AppendLine($"Total price: {Math.Round(totalPrice, 2)} euros");
			return stringBuilder.ToString();


		}
	}
}
