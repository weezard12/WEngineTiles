using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class SelectFileWindow
{
    partial void CustomInitialize()
    {
        SelectButton.Click += (_, _) =>
        {
            RaiseDialogComplete(SelectPathTextBox.Text);
            RemoveFromRoot();
        };
        FilesViewerInstance.OnFileSelected += (filePath) =>
        {
            SelectPathTextBox.Text = filePath;
        };
    }
}
