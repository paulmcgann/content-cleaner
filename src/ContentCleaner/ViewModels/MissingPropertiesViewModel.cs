using EPiServer.DataAbstraction;

namespace ContentCleaner.ViewModels
{
    public class MissingPropertiesViewModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<MissingPropertiesDataViewModel>? data { get; set; }

        public MissingPropertiesViewModel(int draw, int recordsTotal, int recordsFiltered, List<MissingPropertiesDataViewModel> data)
        {
            this.draw = draw;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
            this.data = data;
        }
    }
}