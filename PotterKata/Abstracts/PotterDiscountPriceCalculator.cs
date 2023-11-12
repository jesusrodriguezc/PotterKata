using PotterKata.Data;
using PotterKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PotterKata.Abstracts {
	public class PotterDiscountPriceCalculator : PriceCalculator {

		public override GroupingSolution CalculatePrice (Dictionary<BookID, int> bookQuantity) {

			Dictionary<BookDiscountGroup, int> bestPriceGroupedItems = new Dictionary<BookDiscountGroup, int>();

			double bestPrice = bookQuantity.Sum(book => book.Value * 8f);
			int maxGroupSize = bookQuantity.Count;

			while (maxGroupSize > 1) {

				int maxNumberOfSameBook = bookQuantity.Max(book => book.Value);

				for (int numberOfGroupsWithMaxSize = maxNumberOfSameBook; numberOfGroupsWithMaxSize > 0; numberOfGroupsWithMaxSize--) {
					double currentTotalPrice = 0f;
					var groupedBooks = new Dictionary<BookDiscountGroup, int>();
					var remainingBooks = new Dictionary<BookID, int>(bookQuantity);

					while (remainingBooks.Any()) {

						bool maxGroupsWithMaxSizeReached = groupedBooks
							.Where(group => group.Key.Count == maxGroupSize)
							.Sum(group => group.Value) >= numberOfGroupsWithMaxSize;

						var currentGroup = GetNextGroup(remainingBooks, maxGroupsWithMaxSizeReached ? maxGroupSize - 1 : maxGroupSize);

						currentTotalPrice += GetGroupPrice(currentGroup);

						SubstractGroupFromTotal(currentGroup, remainingBooks);

						groupedBooks[currentGroup] = groupedBooks.ContainsKey(currentGroup) ? groupedBooks[currentGroup] + 1 : 1;

					}

					if (bestPrice > currentTotalPrice) {
						bestPrice = currentTotalPrice;
						bestPriceGroupedItems = groupedBooks;
					}
				}

				maxGroupSize--;
			}

			GroupingSolution groupingSolution = new GroupingSolution() {
				groupsOfBooks = bestPriceGroupedItems,
				totalPrice = bestPrice
			};
			return groupingSolution;
		}

		private void SubstractGroupFromTotal (List<BookID> currentGroup, Dictionary<BookID, int> remainingItems) {
			foreach (var item in currentGroup) {
				remainingItems[item]--;

				if (remainingItems[item] == 0) {
					remainingItems.Remove(item);
				}
			}
		}

		private double GetGroupPrice (List<BookID> groupItems) {
			int groupSize = groupItems.Count;
			double priceWithoutDiscount = groupSize * 8;
			switch (groupSize) {
				case 2:
					return priceWithoutDiscount * 0.95f;
				case 3:
					return priceWithoutDiscount * 0.9f;
				case 4:
					return priceWithoutDiscount * 0.8f;
				case 5:
					return priceWithoutDiscount * 0.75f;
				default:
					return priceWithoutDiscount;
			}
		}
		private BookDiscountGroup GetNextGroup (Dictionary<BookID, int> remainingItems, int maxGroupSize) {
			var booksForDiscount = remainingItems
				.Where(item => item.Value > 0)
				.OrderByDescending(item => item.Value)
				.Take(maxGroupSize)
				.Select(item => item.Key);

			return new BookDiscountGroup(booksForDiscount);
		}

		private string GenerateGroupKey (List<BookID> group) {
			return string.Join(",", group.OrderBy(key => key));
		}

	}
}
