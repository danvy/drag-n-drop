using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.DragDrop.Core;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace DragAndDrop
{
    public sealed partial class MainPage : Page, ICoreDropOperationTarget
    {
		public MainPage()
		{
			this.InitializeComponent();
            var man = CoreDragDropManager.GetForCurrentView();
            man.TargetRequested += Man_TargetRequested;
		}
        private void Man_TargetRequested(CoreDragDropManager sender, CoreDropOperationTargetRequestedEventArgs args)
        {
            args.SetTarget(this);
        }
        public IAsyncOperation<DataPackageOperation> EnterAsync(CoreDragInfo dragInfo, CoreDragUIOverride dragUIOverride)
        {
            Debug.WriteLine("EnterAsync");
            return Task.Run<DataPackageOperation>(() => DataPackageOperation.Copy).AsAsyncOperation<DataPackageOperation>();
        }

        public IAsyncOperation<DataPackageOperation> OverAsync(CoreDragInfo dragInfo, CoreDragUIOverride dragUIOverride)
        {
            Debug.WriteLine("OverAsync");
            return Task.Run<DataPackageOperation>(() => DataPackageOperation.Copy).AsAsyncOperation<DataPackageOperation>();
        }

        public IAsyncAction LeaveAsync(CoreDragInfo dragInfo)
        {
            Debug.WriteLine("LeaveAsync");
            return Task.Run(() => { return; }).AsAsyncAction();
        }

        public IAsyncOperation<DataPackageOperation> DropAsync(CoreDragInfo dragInfo)
        {
            Debug.WriteLine("DropAsync");
            if (dragInfo.Data.Contains("FileNameW"))
            {
                var fileName = dragInfo.Data.GetTextAsync("FileNameW").AsTask<string>().Result;
                FileNameText.Text = fileName;
                Debug.WriteLine("FileName=" + fileName);
            }
            return Task.Run<DataPackageOperation>(() => DataPackageOperation.Copy).AsAsyncOperation<DataPackageOperation>();
        }
    }
}