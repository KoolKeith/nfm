// <copyright file="PanelContainerBase.cs" company="HD">
// 	Copyright (c) 2009 HD. All rights reserved.
// </copyright>
// <author name="Andrew Levshoff">
// 	<email>alevshoff@hd.com</email>
// 	<date>2009-01-10</date>
// </author>
// <editor name="Andrew Levshoff">
// 	<email>alevshoff@hd.com</email>
// 	<date>2009-01-10</date>
// </editor>
// <summary>Base <see cref="IPanelContainer"/> with childs close notifications.</summary>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Nfm.Core.ViewModels
{
	/// <summary>
	/// Base <see cref="IPanelContainer"/> with childs close notifications.
	/// </summary>
	[DebuggerDisplay(
		@"Header = {Header}"
		+ @", Type = {GetType()}"
		+ @", Childs = {Childs.Count}")]
	public class PanelContainerBase : NotificationBase, IPanelContainer
	{
		#region Implementation of IDisposable

		/// <summary>
		/// Forced object distruction.
		/// </summary>
		/// <param name="disposing">"True" for manual calls.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Release managed resources.
				Close(true);
			}

			// Release unmanaged resources.
			// Set large fields to null.
			// Call Dispose on your base class.
			base.Dispose(disposing);

			// The derived class does not have a Finalize method
			// or a Dispose method with parameters because it inherits
			// them from the base class.
		}

		#endregion

		#region Implementation of IPanel

		/// <summary>
		/// Indicating whether a panel is selected.
		/// </summary>
		private bool isSelected;

		/// <summary>
		/// Gets or sets panel header: string text or complex content.
		/// </summary>
		public object Header { get; set; }

		/// <summary>
		/// Gets a value indicating whether a panel can be closed.
		/// </summary>
		public bool CanClose
		{
			get
			{
				foreach (IPanel child in Childs)
				{
					if (!child.CanClose)
					{
						return false;
					}
				}

				return true;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether a panel is selected.
		/// </summary>
		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				OnPropertyChanging("IsSelected");
				isSelected = value;
				OnPropertyChanged("IsSelected");
			}
		}

		/// <summary>
		/// Gets or sets parent <see cref="IPanel"/>.
		/// </summary>
		public IPanel Parent { get; set; }

		/// <summary>
		/// Request close action for panel.
		/// </summary>
		public void RequestClose()
		{
			isClosing = true;
			OnEvent(Closing, this);

			if (Childs.Count > 0)
			{
				Close(false);
			}

			isClosing = false;

			if (Childs.Count == 0)
			{
				OnEvent(Closed, this);
			}
			else
			{
				throw new Exception("Some child panels can not be closed.");
			}
		}

		/// <summary>
		/// Fire when panel is closed.
		/// </summary>
		public virtual event EventHandler<EventArgs> Closed;

		/// <summary>
		/// Fire when panel is intended to close.
		/// </summary>
		public virtual event EventHandler<EventArgs> Closing;

		#region Implementation of ICloneable

		/// <summary>
		/// Creates a new object that is a deep copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a deep copy of this instance.</returns>
		public virtual object Clone()
		{
			return new PanelContainerBase(this);
		}

		#endregion

		#endregion

		#region Implementation of IPanelContainer

		/// <summary>
		/// Child panels.
		/// </summary>
		private ObservableCollection<IPanel> childs;

		/// <summary>
		/// Specifing, when container is in forced closing state.
		/// </summary>
		private bool isClosing;

		/// <summary>
		/// Gets all child panels.
		/// </summary>
		public ObservableCollection<IPanel> Childs
		{
			get
			{
				if (childs == null)
				{
					OnPropertyChanging("Childs");
					childs = new ObservableCollection<IPanel>();
					childs.CollectionChanged += OnChildsChanged;
					OnPropertyChanged("Childs");
				}

				return childs;
			}
		}

		/// <summary>
		/// Close panel and all childs panels.
		/// </summary>
		/// <param name="force">"True" to force close action.</param>
		private void Close(bool force)
		{
			IList<IPanel> closingPanels = new List<IPanel>(Childs);

			foreach (IPanel panel in closingPanels)
			{
				if (panel != null && (force || panel.CanClose))
				{
					panel.RequestClose();
				}
			}

			closingPanels.Clear();
		}

		/// <summary>
		/// Childs panels <see cref="ObservableCollection{T}.CollectionChanged"/> event handler.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event params.</param>
		protected void OnChildsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			// Note: NotifyCollectionChangedAction.Reset
			//    and NotifyCollectionChangedAction.Replace
			//    change types should not occur for ObservableCollection<IPanel>.

			if (e.Action == NotifyCollectionChangedAction.Replace)
			{
				// Todo: remove debug fail.
				Debug.Fail("NotifyCollectionChangedAction.Reset");
			}

			if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				// Todo: remove debug fail.
				Debug.Fail("NotifyCollectionChangedAction.Reset");
			}

			if (e.NewItems != null
				&& e.NewItems.Count > 0
				&& e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (IPanel panel in e.NewItems)
				{
					// Take ownership of this child.
					panel.Parent = this;
					panel.Closed += OnChildClose;
				}
			}

			if (e.OldItems != null
				&& e.OldItems.Count > 0
				&& e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (IPanel panel in e.OldItems)
				{
					// Important: do NOT remove parent from NOT owned childs!
					// When child will be added at the same time to other collection,
					// that new collection will take ownership on this child.
					if (panel.Parent == this)
					{
						panel.Parent = null;
					}

					// But event handler still need to be removed.
					panel.Closed -= OnChildClose;
				}
			}
		}

		/// <summary>
		/// Child <see cref="IPanel.Closed"/> event handler.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event params.</param>
		private void OnChildClose(object sender, EventArgs e)
		{
			if (Childs.Contains(sender as IPanel))
			{
				Childs.Remove(sender as IPanel);
			}

			if (Childs.Count == 0 && !isClosing)
			{
				RequestClose();
			}
		}

		#endregion

		#region .Ctors

		/// <summary>
		/// Initializes a new instance of the <see cref="PanelContainerBase"/> class.
		/// </summary>
		public PanelContainerBase()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PanelContainerBase"/> class.
		/// </summary>
		/// <param name="another">Another <see cref="PanelContainerBase"/> instance to copy data from.</param>
		protected PanelContainerBase(PanelContainerBase another)
		{
			Header = another.Header;
			isSelected = another.isSelected;

			// Detach from parent panel
			//	Parent = null;

			// Remove original subscribiters
			//	Closing = null;	// -= Closing;
			//	Closed = null;	// -= Closed;

			// Deep copy all childs
			var childsCopy = new ObservableCollection<IPanel>();

			// Important: handler below must belong to "result" object and NOT to "this" object!
			// See defect #18.
			childsCopy.CollectionChanged += OnChildsChanged;

			foreach (var child in another.childs)
			{
				var newChild = (IPanel)child.Clone();
				childsCopy.Add(newChild);
			}

			childs = childsCopy;
		}

		#endregion
	}
}