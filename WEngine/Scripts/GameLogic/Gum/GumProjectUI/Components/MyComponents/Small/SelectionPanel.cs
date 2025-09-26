using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

partial class SelectionPanel
{
    private List<FrameworkElement> _frameworkElements;
    private SelectionPanelItemHolder _selectedHolder;
    private int _selectedIndex = -1;

    // Custom events
    public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
    public event EventHandler<ItemClickedEventArgs> ItemClicked;

    partial void CustomInitialize()
    {
        // Initialize the collection
        _frameworkElements = new List<FrameworkElement>();
        Visual.Click += (s, e) =>
        {
            Debug.Log("SelectionPanel clicked");
        };
    }

    public SelectionPanelItemHolder AddItem(GraphicalUiElement item)
    {
        if (item == null) return null;

        SelectionPanelItemHolder holder = new SelectionPanelItemHolder();
        holder.AddChild(item);

        // Add the holder to this panel's visual tree
        this.AddChild(holder);

        // Track the holder in our collection
        _frameworkElements.Add(holder);

        holder.Visual.ExposeChildrenEvents = false;

        holder.Visual.RollOn += (s, e) =>
        {
            Debug.Log("Item hovered");
            // Only apply hover state if not selected
            if (holder != _selectedHolder)
            {
                holder.QuickStylesState = SelectionPanelItemHolder.QuickStyles.Hovered;
            }
        };

        holder.Visual.RollOff += (s, e) =>
        {
            // Only clear hover if not selected
            if (holder != _selectedHolder)
            {
                holder.QuickStylesState = SelectionPanelItemHolder.QuickStyles.Clear;
            }
        };

        holder.Visual.Click += (s, e) =>
        {
            int clickedIndex = GetHolderIndex(holder);
            Debug.Log($"Item clicked at index: {clickedIndex}");

            // Fire item clicked event
            ItemClicked?.Invoke(this, new ItemClickedEventArgs(holder, clickedIndex));

            // Handle selection
            SelectItem(clickedIndex);
        };

        // Auto-select the first item when it's added
        if (_frameworkElements.Count == 1)
        {
            SelectItem(0);
        }
        return holder;
    }

    public SelectionPanelItemHolder AddItem(FrameworkElement item)
    {
        if (item == null) return null;

        SelectionPanelItemHolder holder = new SelectionPanelItemHolder(item);
        holder.AddChild(item);

        // Add the holder to this panel's visual tree
        this.AddChild(holder);

        // Track the holder in our collection
        _frameworkElements.Add(holder);

        holder.Visual.ExposeChildrenEvents = false;

        holder.Visual.RollOn += (s, e) =>
        {
            Debug.Log("Item hovered");
            // Only apply hover state if not selected
            if (holder != _selectedHolder)
            {
                holder.QuickStylesState = SelectionPanelItemHolder.QuickStyles.Hovered;
            }
        };

        holder.Visual.RollOff += (s, e) =>
        {
            // Only clear hover if not selected
            if (holder != _selectedHolder)
            {
                holder.QuickStylesState = SelectionPanelItemHolder.QuickStyles.Clear;
            }
        };

        holder.Visual.Click += (s, e) =>
        {
            int clickedIndex = GetHolderIndex(holder);
            Debug.Log($"Item clicked at index: {clickedIndex}");

            // Fire item clicked event
            ItemClicked?.Invoke(this, new ItemClickedEventArgs(holder, clickedIndex));

            // Handle selection
            SelectItem(clickedIndex);
        };

        // Auto-select the first item when it's added
        if (_frameworkElements.Count == 1)
        {
            SelectItem(0);
        }
        return holder;
    }

    public void RemoveItem(GraphicalUiElement item)
    {
        if (item == null) return;

        // Find the holder that contains this GraphicalUiElement
        var holderToRemove = _frameworkElements
            .OfType<SelectionPanelItemHolder>()
            .FirstOrDefault(holder => holder.Visual.Children.Contains(item));

        if (holderToRemove != null)
        {
            int removedIndex = GetHolderIndex(holderToRemove);

            // If we're removing the selected item, clear selection
            if (holderToRemove == _selectedHolder)
            {
                ClearSelection();
            }
            // If we're removing an item before the selected one, adjust selected index
            else if (_selectedIndex > removedIndex)
            {
                _selectedIndex--;
            }

            // Remove the item from the holder
            holderToRemove.RemoveFromRoot();
            _frameworkElements.Remove(holderToRemove);
        }
    }
    public void RemoveItem(FrameworkElement item)
    {
        if (item == null) return;

        // Find the holder that contains this item
        var holderToRemove = _frameworkElements
            .OfType<SelectionPanelItemHolder>()
            .FirstOrDefault(holder => holder.Visual.Children.Contains(item.Visual));

        if (holderToRemove != null)
        {
            int removedIndex = GetHolderIndex(holderToRemove);

            // If we're removing the selected item, clear selection
            if (holderToRemove == _selectedHolder)
            {
                ClearSelection();
            }
            // If we're removing an item before the selected one, adjust selected index
            else if (_selectedIndex > removedIndex)
            {
                _selectedIndex--;
            }

            // Remove the item from the holder
            holderToRemove.RemoveFromRoot();
            _frameworkElements.Remove(holderToRemove);
        }
    }

    public void ClearItems()
    {
        // Clear selection first
        ClearSelection();

        // Remove all items
        var holdersToRemove = _frameworkElements.OfType<SelectionPanelItemHolder>().ToList();
        foreach (var holder in holdersToRemove)
            holder.RemoveFromRoot();
        _frameworkElements.Clear();
    }

    public void SelectItem(int index)
    {
        if (index < 0 || index >= _frameworkElements.Count)
        {
            ClearSelection();
            return;
        }

        var newSelectedHolder = _frameworkElements[index] as SelectionPanelItemHolder;
        if (newSelectedHolder == null) return;

        var previousSelection = _selectedHolder;
        var previousIndex = _selectedIndex;

        // Clear previous selection visual state
        if (_selectedHolder != null && _selectedHolder != newSelectedHolder)
        {
            _selectedHolder.QuickStylesState = SelectionPanelItemHolder.QuickStyles.Clear;
        }

        // Set new selection
        _selectedHolder = newSelectedHolder;
        _selectedIndex = index;
        _selectedHolder.QuickStylesState = SelectionPanelItemHolder.QuickStyles.Selected;

        // Fire selection changed event
        SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(
            previousSelection, previousIndex, _selectedHolder, _selectedIndex));
    }

    public void ClearSelection()
    {
        if (_selectedHolder != null)
        {
            var previousSelection = _selectedHolder;
            var previousIndex = _selectedIndex;

            _selectedHolder.QuickStylesState = SelectionPanelItemHolder.QuickStyles.Clear;
            _selectedHolder = null;
            _selectedIndex = -1;

            // Fire selection changed event
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(
                previousSelection, previousIndex, null, -1));
        }
    }

    public void SelectItemByElement(FrameworkElement element)
    {
        var holder = _frameworkElements
            .OfType<SelectionPanelItemHolder>()
            .FirstOrDefault(h => h.Visual.Children.Contains(element.Visual));

        if (holder != null)
        {
            int index = GetHolderIndex(holder);
            SelectItem(index);
        }
    }

    private int GetHolderIndex(SelectionPanelItemHolder holder)
    {
        return _frameworkElements.IndexOf(holder);
    }

    // Properties for accessing selection
    public SelectionPanelItemHolder SelectedItem => _selectedHolder;
    public int SelectedIndex => _selectedIndex;
    public bool HasSelection => _selectedHolder != null;
    public int ItemCount => _frameworkElements.Count;

    // Get the actual framework element that was added (not the holder)
    public FrameworkElement GetSelectedFrameworkElement()
    {
        if (_selectedHolder?.Visual.Children.Count > 0)
        {
            var childVisual = _selectedHolder.Visual.Children[0];
            // Find the FrameworkElement that corresponds to this visual
            return _frameworkElements
                .Where(fe => fe.Visual == childVisual)
                .FirstOrDefault();
        }
        return null;
    }

    public FrameworkElement GetItemAt(int index)
    {
        if (index < 0 || index >= _frameworkElements.Count) return null;

        var holder = _frameworkElements[index] as SelectionPanelItemHolder;
        if (holder?.Visual.Children.Count > 0)
        {
            var childVisual = holder.Visual.Children[0];
            return _frameworkElements
                .Where(fe => fe.Visual == childVisual)
                .FirstOrDefault();
        }
        return null;
    }
}

// TODO Move this to a seperate namespace?
// Event argument classes
class SelectionChangedEventArgs : EventArgs
{
    public SelectionPanelItemHolder PreviousSelection { get; }
    public int PreviousIndex { get; }
    public SelectionPanelItemHolder NewSelection { get; }
    public int NewIndex { get; }

    public SelectionChangedEventArgs(SelectionPanelItemHolder previousSelection, int previousIndex,
                                   SelectionPanelItemHolder newSelection, int newIndex)
    {
        PreviousSelection = previousSelection;
        PreviousIndex = previousIndex;
        NewSelection = newSelection;
        NewIndex = newIndex;
    }
}

class ItemClickedEventArgs : EventArgs
{
    public SelectionPanelItemHolder ClickedItem { get; }
    public int Index { get; }

    public ItemClickedEventArgs(SelectionPanelItemHolder clickedItem, int index)
    {
        ClickedItem = clickedItem;
        Index = index;
    }
}