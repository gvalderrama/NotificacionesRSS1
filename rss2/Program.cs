using System;
using Gtk;
using System.Xml;

namespace Rss2
{
	class testRSS : Window    {
		XmlDocument rssDoc;
		XmlNode nodeRss;
		XmlNode nodeChannel;
		XmlNode nodeItem;
		Gtk.TreeStore musicListStore = new Gtk.TreeStore(typeof(string), typeof(string));
		public testRSS(): base("test"){
			int no = 1;
			Gtk.Window window = new Gtk.Window("RSS JCares");
			window.SetSizeRequest(500, 500);
			window.SetPosition(WindowPosition.Center);
			Gtk.TreeView tree = new Gtk.TreeView();
			Gtk.TreeViewColumn rssIcerikColumn = new Gtk.TreeViewColumn();
			rssIcerikColumn.Title = "RSS feeds";
			tree.AppendColumn(rssIcerikColumn);

			Gtk.ListStore rssStore = new Gtk.ListStore(typeof(string));
			Gtk.TreeStore musicListStore = new Gtk.TreeStore(typeof(string), typeof(string));
			XmlTextReader reader = new XmlTextReader("http://rss.emol.com/rss.asp");
			rssDoc = new XmlDocument();
			rssDoc.Load(reader);
			for (int i = 0; i < rssDoc.ChildNodes.Count; i++) {
				if (rssDoc.ChildNodes[i].Name == "rss")                {
					nodeRss = rssDoc.ChildNodes[i];                }
			}                      for (int i = 0; i < nodeRss.ChildNodes.Count; i++)            {
				if (nodeRss.ChildNodes[i].Name == "channel")                {
					nodeChannel = nodeRss.ChildNodes[i];                }
			}                                   for (int i = 0; i < nodeChannel.ChildNodes.Count; i++)
			{                                       if (nodeChannel.ChildNodes[i].Name == "item")
				{                        nodeItem = nodeChannel.ChildNodes[i];
					Gtk.TreeIter iter = musicListStore.AppendValues(nodeItem["title"].InnerText);
					if (nodeChannel.ChildNodes[i].Name == "item")                        {                                                      nodeItem = nodeChannel.ChildNodes[i];                                                       musicListStore.AppendValues(iter, nodeItem["description"].InnerText);
					}

					tree.Model = musicListStore;
					no++;                    }
			}
			Gtk.CellRendererText rssNoCell = new Gtk.CellRendererText();

			Gtk.CellRendererText rssIcerikCell = new Gtk.CellRendererText();
			rssIcerikColumn.PackStart(rssIcerikCell, true);
			rssIcerikColumn.AddAttribute(rssIcerikCell, "text", 0);

			//Image  logo = new Gtk.Image("rss.gif");
			Button denemeButton = new Button();
			denemeButton.Label = "Exit";
			denemeButton.Clicked += new EventHandler(denemeButton_Clicked);
			Fixed fixed1 = new Fixed();
			fixed1.SetSizeRequest(500,480);
			//fixed1.Put(logo, 0, 0);
			Entry Enlace = new Gtk.Entry();
			Enlace.MaxLength = 50;
			Button GenerarRSS = new Button();
			GenerarRSS.Label = "Buscar";
			GenerarRSS.Clicked += delegate(object sender, EventArgs e) {
				generarRSS_Clicked (sender, e, Enlace.Text);
			};

			fixed1.Put(Enlace, 0, 0);
			fixed1.Put(GenerarRSS, 200, 0);
			fixed1.Put(denemeButton, 460, 0);
			ScrolledWindow scroll = new ScrolledWindow();
			scroll.SetSizeRequest(500, 500);
			scroll.Add(tree);
			fixed1.Put(scroll, 0, 30);
			window.Add(fixed1);
			window.ShowAll();
		}

		void denemeButton_Clicked(object sender, EventArgs e)        {
			Application.Quit();
		}
		public void generarRSS_Clicked(object sender, EventArgs e, string url){
			System.Console.WriteLine (url);		
		}
		public static void Main()        {
			Application.Init();
			new testRSS();
			Application.Run();
		}

	}
}