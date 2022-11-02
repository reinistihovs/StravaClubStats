namespace StravaClubStatsBlazorServerApp.Helpers
{
    public class TableSortHelper<T>
    {
        public List<T> ListToSort { get; set; }

        private bool isSortedAscending;

        private string currentSortColumn;

        public TableSortHelper(List<T> listToSort)
        {
            ListToSort = listToSort;
        }

        public string GetSortStyle(string columnName)
        {
            if (currentSortColumn != columnName)
            {
                return string.Empty;
            }

            return isSortedAscending ? "▲" : "▼";
        }

        public void SortTable(string columnName)
        {
            if (columnName != currentSortColumn)
            {
                ListToSort = ListToSort
                                .OrderBy(x =>
                                    x.GetType()
                                    .GetProperty(columnName)
                                    .GetValue(x, null))
                                .ToList();

                currentSortColumn = columnName;
                isSortedAscending = true;

            }
            else
            {
                if (isSortedAscending)
                {
                    ListToSort = ListToSort
                                    .OrderByDescending(x =>
                                                    x.GetType()
                                                    .GetProperty(columnName)
                                                    .GetValue(x, null))
                                    .ToList();
                }
                else
                {
                    ListToSort = ListToSort
                                    .OrderBy(x =>
                                        x.GetType()
                                            .GetProperty(columnName)
                                            .GetValue(x, null))
                                    .ToList();
                }

                isSortedAscending = !isSortedAscending;
            }
        }
    }
}
