using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewFileExplorerLibrary.Models
{
    /// <summary>
    /// Represents a single folder with defined size unit.
    /// </summary>
    public class SizedFolder : IFolderModel
    {
        private const int BytesInKilobyte = 1024;
        private const int BytesInMegabyte = 1048576;
        private const int BytesInGigabyte = 1073741824;

        private readonly IFolderModel _decoratedFolderModel;

        public string ImageUri 
        {
            get => _decoratedFolderModel.ImageUri; 
            set => _decoratedFolderModel.ImageUri = value; 
        }
        public string Name 
        { 
            get => _decoratedFolderModel.Name; 
            set => _decoratedFolderModel.Name = value; 
        }
        public long Size 
        { 
            get => _decoratedFolderModel.Size; 
            set => _decoratedFolderModel.Size = value; 
        }
        public List<ITreeComponent> ChildTreeItems 
        { 
            get => _decoratedFolderModel.ChildTreeItems; 
            set => _decoratedFolderModel.ChildTreeItems = value; 
        }
        public string SizeForShow
        {
            get => _decoratedFolderModel.SizeForShow;
            set => _decoratedFolderModel.SizeForShow = value;
        }

        public SizedFolder(IFolderModel folder)
        {
            _decoratedFolderModel = folder;
        }

        public void CalculateSize()
        {
            Size = ChildTreeItems.Sum(x => x.Size);
        }

        /// <summary>
        /// Sets appropriate folder size unit based on folder 
        /// size in bytes (used for UI purposes).
        /// </summary>
        public void SetSizeUnits()
        {
            string size = Size.ToString();
            string sizeUnit = "B";

            if (Size > BytesInKilobyte && Size < BytesInMegabyte)
            {
                decimal value = (decimal)Size / BytesInKilobyte;
                size = Math.Round(value, MidpointRounding.AwayFromZero).ToString();
                sizeUnit = "KB";
            }
            else if (Size > BytesInMegabyte && Size < BytesInGigabyte)
            {
                decimal value = (decimal)Size / BytesInMegabyte;
                size = Math.Round(value, MidpointRounding.AwayFromZero).ToString();
                sizeUnit = "MB";
            }
            else if (Size > BytesInGigabyte)
            {
                decimal value = (decimal)Size / BytesInGigabyte;
                size = Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString();
                sizeUnit = "GB";
            }

            SizeForShow = $"({size} {sizeUnit})";
        }
    }
}
