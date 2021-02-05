using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewFileExplorerLibrary.Models
{
    /// <summary>
    /// Represents a single file with defined size unit.
    /// </summary>
    public class SizedFile : ITreeComponent
    {
        private const int BytesInKilobyte = 1024;
        private const int BytesInMegabyte = 1048576;
        private const int BytesInGigabyte = 1073741824;

        private readonly ITreeComponent _decoratedTreeItemModel;

        public string ImageUri 
        {
            get => _decoratedTreeItemModel.ImageUri;
            set => _decoratedTreeItemModel.ImageUri = value;
        }
        public string Name 
        {
            get => _decoratedTreeItemModel.Name;
            set => _decoratedTreeItemModel.Name = value;
        }
        public long Size 
        {
            get => _decoratedTreeItemModel.Size;
            set => _decoratedTreeItemModel.Size = value;
        }
        public string SizeForShow 
        {
            get => _decoratedTreeItemModel.SizeForShow;
            set => _decoratedTreeItemModel.SizeForShow = value;
        }

        public SizedFile(ITreeComponent fileModel)
        {
            _decoratedTreeItemModel = fileModel;
        }

        /// <summary>
        /// Sets appropriate file size unit based on file 
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
