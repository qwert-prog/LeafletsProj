using IoC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;

namespace Utils.Services
{
    /// <summary>
    /// Service for save data into different formats
    /// </summary>
    public class SaveDataService
    {
        #region Public Methods

        /// <summary>
        /// Method for save data to JPEG
        /// </summary>
        public async Task SaveToJPEG(UIElement element)
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "ResultToJPEG"
            };
            savePicker.FileTypeChoices.Add("JPEG", new List<string>() { ".jpeg" });

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    RenderUIElementService renderUIElement = ServicesContainer.GetService<RenderUIElementService>();
                    await renderUIElement.ExportIntoStreamAsync(stream, element);
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                    BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(stream, decoder);
                    await encoder.FlushAsync();
                }
            }
        }

        /// <summary>
        /// Method for save data to PNG
        /// </summary>
        public async Task SaveToPNG(UIElement element)
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "ResultToPNG"
            };

            savePicker.FileTypeChoices.Add("PNG image", new List<string>() { ".png" });

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    RenderUIElementService renderUIElement = ServicesContainer.GetService<RenderUIElementService>();
                    await renderUIElement.ExportIntoStreamAsync(stream, element);
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                    BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(stream, decoder);
                    await encoder.FlushAsync();
                }
            }
        }

        #endregion Public Methods
    }
}