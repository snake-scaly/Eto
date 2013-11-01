using System;
using System.Collections.Generic;

#if XAML
using System.Windows.Markup;
#endif

namespace Eto.Forms
{
	public interface ITabControl : IContainer
	{
		int SelectedIndex { get; set; }

		void InsertTab(int index, TabPage page);
		
		void ClearTabs();

		void RemoveTab(int index, TabPage page);
	}
	
	[ContentProperty("TabPages")]
	public class TabControl : Container
	{
		readonly TabPageCollection pages;
		new ITabControl Handler { get { return (ITabControl)base.Handler; } }

		public override IEnumerable<Control> Controls
		{
			get { return pages; }
		}
		
		public event EventHandler<EventArgs> SelectedIndexChanged;

		public virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, e);
		}
		
		public TabControl() : this (Generator.Current)
		{
		}

		public TabControl(Generator g) : this (g, typeof(ITabControl))
		{
		}
		
		protected TabControl(Generator generator, Type type, bool initialize = true)
			: base (generator, type, initialize)
		{
			pages = new TabPageCollection(this);
		}

		protected TabControl(Generator generator, ITabControl handler, bool initialize = true)
			: base(generator, handler, initialize)
		{
			pages = new TabPageCollection(this);
		}

		public int SelectedIndex
		{
			get { return Handler.SelectedIndex; }
			set { Handler.SelectedIndex = value; }
		}
		
		public TabPage SelectedPage
		{
			get { return SelectedIndex < 0 ? null : TabPages[SelectedIndex]; }
			set { SelectedIndex = pages.IndexOf(value); }
		}

		public TabPageCollection TabPages
		{
			get { return pages; }
		}

		internal void InsertTab(int index, TabPage page)
		{
			if (Loaded)
			{
				page.OnPreLoad(EventArgs.Empty);
				page.OnLoad(EventArgs.Empty);
				page.OnLoadComplete(EventArgs.Empty);
			}
			SetParent(page);
			Handler.InsertTab(index, page);
		}

		internal void RemoveTab(int index, TabPage page)
		{
			Handler.RemoveTab(index, page);
			RemoveParent(page, true);
		}
		
		internal void ClearTabs()
		{
			Handler.ClearTabs();
		}

		public override void Remove(Control child)
		{
			var childPage = child as TabPage;
			if (childPage != null)
			{
				var index = pages.IndexOf(childPage);
				if (index >= 0)
				{
					RemoveTab(index, childPage);
					RemoveParent(childPage, true);
				}
			}
		}

		public ObjectBinding<TabControl, int> SelectedIndexBinding
		{
			get
			{
				return new ObjectBinding<TabControl, int>(
					this, 
					c => c.SelectedIndex, 
					(c, v) => c.SelectedIndex = v, 
					(c, h) => c.SelectedIndexChanged += h, 
					(c, h) => c.SelectedIndexChanged -= h
				);
			}
		}
	}
}
