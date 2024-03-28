namespace ContentCleaner.ViewModels
{
    public class ContentUsageViewModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ContentUsageDataViewModel>? data { get; set; }

        public ContentUsageViewModel(int draw, int recordsTotal, int recordsFiltered, List<ContentUsageDataViewModel> data)
        {
            this.draw = draw;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
            this.data = data;
        }
    }
}