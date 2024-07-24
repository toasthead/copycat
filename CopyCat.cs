#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

// Title: CopyCat Trade Copier
// Origination Based on Simple Trade Copier https://ninjatraderecosystem.com/user-app-share-download/simple-trade-copier/
// International License: CC BY-NC-SA 4.0 ATTRIBUTION-NONCOMMERCIAL-SHAREALIKE 4.0 INTERNATIONAL
// License Link: https://creativecommons.org/licenses/by-nc-sa/4.0/deed.en
// License Details: 
//	You are free to:
//		Share — copy and redistribute the material in any medium or format
//		Adapt — remix, transform, and build upon the material
//		The licensor cannot revoke these freedoms as long as you follow the license terms.
//	Under the following terms:
//		Attribution — You must give appropriate credit , provide a link to the license, and indicate if changes were made . You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
//		NonCommercial — You may not use the material for commercial purposes .
//		ShareAlike — If you remix, transform, or build upon the material, you must distribute your contributions under the same license as the original.
//		No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.

// HISTORY
// 2024.07.10 - Origination

// TODO
// Prevent Master from being selected as a child.
// Add ability to clear an entry.

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.Toasthedge
{
	public class CopyCat : Indicator
	{
		
		private Chart chartWindow;
		
		private bool copyButtonClicked;
		private System.Windows.Controls.Button copyButton;
		private System.Windows.Controls.Grid myGrid;
		
		private bool IsCopyAllowed = false;
		
		//=== Accounts 10 + Master
		private Account MasterAccount = null;
		
		private Account Acc1= null;
		private Account Acc2= null;
		private Account Acc3= null;
		private Account Acc4= null;
		private Account Acc5= null;
		
		private Account Acc6= null;
		private Account Acc7= null;
		private Account Acc8= null;
		private Account Acc9= null;
		private Account Acc10= null;
		
		private Account Acc11= null;
		private Account Acc12= null;
		private Account Acc13= null;
		private Account Acc14= null;
		private Account Acc15= null;
		
		private Account Acc16= null;
		private Account Acc17= null;
		private Account Acc18= null;
		private Account Acc19= null;
		private Account Acc20= null;
		
		private Order	BuyOrder;
		private Order	SellOrder;
		
		private string lastOrderID = "";
		Position myPosition = null;
		
		OrderAction orderAction;
		OrderType orderType = OrderType.Market;
		private int ordQuantity;
		
		Execution masterExecution = null;
		private Position masterPosition = null;
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"CopyCat v.1.00.01 - An absolutely free trade copier.";
				Name										= "CopyCat v.1.00.01";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= false;
				DrawOnPricePanel							= false;
				DrawHorizontalGridLines						= false;
				DrawVerticalGridLines						= false;
				PaintPriceMarkers							= false;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				
				
				ThisTool = "https://github.com/toasthead/copycat";
				
			}
			else if (State == State.Configure)
			{
				IsCopyAllowed = false;
			}
			else if(State == State.DataLoaded)
			{
				#region Accounts
				// Account select
				lock (Account.All)
					if (MasterAccount_name != null  ) MasterAccount = Account.All.FirstOrDefault(a => a.Name == MasterAccount_name);
					if (Accountname_1 != null  ) Acc1 = Account.All.FirstOrDefault(a => a.Name == Accountname_1);
					if (Accountname_2 != null  ) Acc2 = Account.All.FirstOrDefault(a => a.Name == Accountname_2);
					if (Accountname_3 != null  ) Acc3 = Account.All.FirstOrDefault(a => a.Name == Accountname_3);
					if (Accountname_4 != null  ) Acc4 = Account.All.FirstOrDefault(a => a.Name == Accountname_4);
					if (Accountname_5 != null  ) Acc5 = Account.All.FirstOrDefault(a => a.Name == Accountname_5);
					if (Accountname_6 != null  ) Acc6 = Account.All.FirstOrDefault(a => a.Name == Accountname_6);
					if (Accountname_7 != null  ) Acc7 = Account.All.FirstOrDefault(a => a.Name == Accountname_7);
					if (Accountname_8 != null  ) Acc8 = Account.All.FirstOrDefault(a => a.Name == Accountname_8);
					if (Accountname_9 != null  ) Acc9 = Account.All.FirstOrDefault(a => a.Name == Accountname_9);
					if (Accountname_10 != null  ) Acc10 = Account.All.FirstOrDefault(a => a.Name == Accountname_10);
					if (Accountname_11 != null  ) Acc11 = Account.All.FirstOrDefault(a => a.Name == Accountname_11);
					if (Accountname_12 != null  ) Acc12 = Account.All.FirstOrDefault(a => a.Name == Accountname_12);
					if (Accountname_13 != null  ) Acc13 = Account.All.FirstOrDefault(a => a.Name == Accountname_13);
					if (Accountname_14 != null  ) Acc14 = Account.All.FirstOrDefault(a => a.Name == Accountname_14);
					if (Accountname_15 != null  ) Acc15 = Account.All.FirstOrDefault(a => a.Name == Accountname_15);
					if (Accountname_16 != null  ) Acc16 = Account.All.FirstOrDefault(a => a.Name == Accountname_16);
					if (Accountname_17 != null  ) Acc17 = Account.All.FirstOrDefault(a => a.Name == Accountname_17);
					if (Accountname_18 != null  ) Acc18 = Account.All.FirstOrDefault(a => a.Name == Accountname_18);
					if (Accountname_19 != null  ) Acc19 = Account.All.FirstOrDefault(a => a.Name == Accountname_19);
					if (Accountname_20 != null  ) Acc20 = Account.All.FirstOrDefault(a => a.Name == Accountname_20);
					
					
			
				
					if (MasterAccount != null) MasterAccount.OrderUpdate 	+= OnOrderUpdate;
					if (MasterAccount != null) MasterAccount.PositionUpdate 	+= OnPositionUpdate;
					if (MasterAccount != null) MasterAccount.ExecutionUpdate 	+= OnExecutionUpdate;
					
					if (Acc1 != null) 	{Acc1.PositionUpdate 	+= OnPositionUpdate; Acc1.OrderUpdate 	+= OnOrderUpdate; Acc1.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc2 != null) 	{Acc2.PositionUpdate 	+= OnPositionUpdate; Acc2.OrderUpdate 	+= OnOrderUpdate; Acc2.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc3 != null) 	{Acc3.PositionUpdate 	+= OnPositionUpdate; Acc3.OrderUpdate 	+= OnOrderUpdate; Acc3.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc4 != null) 	{Acc4.PositionUpdate 	+= OnPositionUpdate; Acc4.OrderUpdate 	+= OnOrderUpdate; Acc4.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc5 != null) 	{Acc5.PositionUpdate 	+= OnPositionUpdate; Acc5.OrderUpdate 	+= OnOrderUpdate; Acc5.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc6 != null) 	{Acc6.PositionUpdate 	+= OnPositionUpdate; Acc6.OrderUpdate 	+= OnOrderUpdate; Acc6.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc7 != null) 	{Acc7.PositionUpdate 	+= OnPositionUpdate; Acc7.OrderUpdate 	+= OnOrderUpdate; Acc7.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc8 != null) 	{Acc8.PositionUpdate 	+= OnPositionUpdate; Acc8.OrderUpdate 	+= OnOrderUpdate; Acc8.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc9 != null) 	{Acc9.PositionUpdate 	+= OnPositionUpdate; Acc9.OrderUpdate 	+= OnOrderUpdate; Acc9.ExecutionUpdate 	+= OnExecutionUpdate;}
					if (Acc10 != null) 	{Acc10.PositionUpdate 	+= OnPositionUpdate; Acc10.OrderUpdate 	+= OnOrderUpdate; Acc10.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc11 != null) 	{Acc11.PositionUpdate 	+= OnPositionUpdate; Acc11.OrderUpdate 	+= OnOrderUpdate; Acc11.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc12 != null) 	{Acc12.PositionUpdate 	+= OnPositionUpdate; Acc12.OrderUpdate 	+= OnOrderUpdate; Acc12.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc13 != null) 	{Acc13.PositionUpdate 	+= OnPositionUpdate; Acc13.OrderUpdate 	+= OnOrderUpdate; Acc13.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc14 != null) 	{Acc14.PositionUpdate 	+= OnPositionUpdate; Acc14.OrderUpdate 	+= OnOrderUpdate; Acc14.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc15 != null) 	{Acc15.PositionUpdate 	+= OnPositionUpdate; Acc15.OrderUpdate 	+= OnOrderUpdate; Acc15.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc16 != null) 	{Acc16.PositionUpdate 	+= OnPositionUpdate; Acc16.OrderUpdate 	+= OnOrderUpdate; Acc16.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc17 != null) 	{Acc17.PositionUpdate 	+= OnPositionUpdate; Acc17.OrderUpdate 	+= OnOrderUpdate; Acc17.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc18 != null) 	{Acc18.PositionUpdate 	+= OnPositionUpdate; Acc18.OrderUpdate 	+= OnOrderUpdate; Acc18.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc19 != null) 	{Acc19.PositionUpdate 	+= OnPositionUpdate; Acc19.OrderUpdate 	+= OnOrderUpdate; Acc19.ExecutionUpdate += OnExecutionUpdate;}
					if (Acc20 != null) 	{Acc20.PositionUpdate 	+= OnPositionUpdate; Acc20.OrderUpdate 	+= OnOrderUpdate; Acc20.ExecutionUpdate += OnExecutionUpdate;}
	
					
					if (MasterAccount != null ) 
						foreach (Position pos in MasterAccount.Positions)
						{
								if ( pos.Instrument == Instrument && pos.Account == MasterAccount )
								masterPosition = pos;
						}
				#endregion
				
				
			}
			else if (State == State.Historical)
			{
				if (UserControlCollection.Contains(myGrid))
					return;
				
				Dispatcher.InvokeAsync((() =>
				{
					myGrid = new System.Windows.Controls.Grid
					{
						Name = "MyCustomGrid", HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom
					};
					System.Windows.Controls.ColumnDefinition column1 = new System.Windows.Controls.ColumnDefinition();
					myGrid.ColumnDefinitions.Add(column1);
					copyButton = new System.Windows.Controls.Button
					{
						Name = "Copy", Content = "Copy OFF", Foreground = Brushes.White, Background = Brushes.Maroon
					};					
					copyButton.Click += OnButtonClick;
					System.Windows.Controls.Grid.SetColumn(copyButton, 0);
					myGrid.Children.Add(copyButton);
					UserControlCollection.Add(myGrid);
				}));
				//=========================
				
			}
			else if (State == State.Terminated)
			{	
				IsCopyAllowed = false;
				Dispatcher.InvokeAsync((() =>
				{
					if (myGrid != null)
					{
						if (copyButton != null)
						{
							myGrid.Children.Remove(copyButton);
							copyButton.Click -= OnButtonClick;
							copyButton = null;
						}
					}
				}));
				//---
				if (MasterAccount != null) MasterAccount.OrderUpdate 	-= OnOrderUpdate;
				if (MasterAccount != null) MasterAccount.PositionUpdate 	-= OnPositionUpdate;
				if (MasterAccount != null) MasterAccount.ExecutionUpdate 	-= OnExecutionUpdate;
			
					if (Acc1 != null) 	{Acc1.PositionUpdate 	-= OnPositionUpdate; Acc1.OrderUpdate 	-= OnOrderUpdate; Acc1.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc2 != null) 	{Acc2.PositionUpdate 	-= OnPositionUpdate; Acc2.OrderUpdate 	-= OnOrderUpdate; Acc2.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc3 != null) 	{Acc3.PositionUpdate 	-= OnPositionUpdate; Acc3.OrderUpdate 	-= OnOrderUpdate; Acc3.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc4 != null) 	{Acc4.PositionUpdate 	-= OnPositionUpdate; Acc4.OrderUpdate 	-= OnOrderUpdate; Acc4.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc5 != null) 	{Acc5.PositionUpdate 	-= OnPositionUpdate; Acc5.OrderUpdate 	-= OnOrderUpdate; Acc5.ExecutionUpdate 	-= OnExecutionUpdate;}
					
					if (Acc6 != null) 	{Acc6.PositionUpdate 	-= OnPositionUpdate; Acc6.OrderUpdate 	-= OnOrderUpdate; Acc6.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc7 != null) 	{Acc7.PositionUpdate 	-= OnPositionUpdate; Acc7.OrderUpdate 	-= OnOrderUpdate; Acc7.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc8 != null) 	{Acc8.PositionUpdate 	-= OnPositionUpdate; Acc8.OrderUpdate 	-= OnOrderUpdate; Acc8.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc9 != null) 	{Acc9.PositionUpdate 	-= OnPositionUpdate; Acc9.OrderUpdate 	-= OnOrderUpdate; Acc9.ExecutionUpdate 	-= OnExecutionUpdate;}
					if (Acc10 != null) 	{Acc10.PositionUpdate 	-= OnPositionUpdate; Acc10.OrderUpdate 	-= OnOrderUpdate; Acc10.ExecutionUpdate -= OnExecutionUpdate;}
					
					if (Acc11 != null) 	{Acc11.PositionUpdate 	-= OnPositionUpdate; Acc11.OrderUpdate 	-= OnOrderUpdate; Acc11.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc12 != null) 	{Acc12.PositionUpdate 	-= OnPositionUpdate; Acc12.OrderUpdate 	-= OnOrderUpdate; Acc12.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc13 != null) 	{Acc13.PositionUpdate 	-= OnPositionUpdate; Acc13.OrderUpdate 	-= OnOrderUpdate; Acc13.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc14 != null) 	{Acc14.PositionUpdate 	-= OnPositionUpdate; Acc14.OrderUpdate 	-= OnOrderUpdate; Acc14.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc15 != null) 	{Acc15.PositionUpdate 	-= OnPositionUpdate; Acc15.OrderUpdate 	-= OnOrderUpdate; Acc15.ExecutionUpdate -= OnExecutionUpdate;}
					
					if (Acc16 != null) 	{Acc16.PositionUpdate 	-= OnPositionUpdate; Acc16.OrderUpdate 	-= OnOrderUpdate; Acc16.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc17 != null) 	{Acc17.PositionUpdate 	-= OnPositionUpdate; Acc17.OrderUpdate 	-= OnOrderUpdate; Acc17.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc18 != null) 	{Acc18.PositionUpdate 	-= OnPositionUpdate; Acc18.OrderUpdate 	-= OnOrderUpdate; Acc18.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc19 != null) 	{Acc19.PositionUpdate 	-= OnPositionUpdate; Acc19.OrderUpdate 	-= OnOrderUpdate; Acc19.ExecutionUpdate -= OnExecutionUpdate;}
					if (Acc20 != null) 	{Acc20.PositionUpdate 	-= OnPositionUpdate; Acc20.OrderUpdate 	-= OnOrderUpdate; Acc20.ExecutionUpdate -= OnExecutionUpdate;}
					
			}
			else if (State == State.Realtime)
			{
			// this will disable copy if you change current window/chart 
//				if (ChartControl != null )
//				{
//					Dispatcher.InvokeAsync((Action)(() => 
//					    {
//							chartWindow = Window.GetWindow(this.ChartControl.Parent) as Chart;
//						}));
					
//					if (!chartWindow.IsFocused) IsCopyAllowed = false;
//				}
			}
		}
		//---
		public override string DisplayName
		{
			get { return " Cat: " + this.MasterAccount_name + " - " + this.Name;}
		}
		//---
		
		private void OnButtonClick(object sender, RoutedEventArgs rea)
		{
			
			System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
			if (button == copyButton && button.Name == "Copy" && button.Content == "Copy OFF")
			{
				button.Content = "Copying...ON";
				button.Background = Brushes.DarkGreen;
				copyButtonClicked = true;
				IsCopyAllowed = true;
				return;
			}
			
			if (button == copyButton && button.Name == "Copy" && button.Content == "Copying...ON")
			{
				button.Content = "Copy OFF";
				button.Background = Brushes.Maroon;
				copyButtonClicked = false;
				IsCopyAllowed = false;
				return;
			}			
		}
		
		private void OnAccountItemUpdate(object sender, AccountItemEventArgs e){}	
		private void OnExecutionUpdate(object sender, ExecutionEventArgs e){}			
	    private void OnPositionUpdate(object sender, PositionEventArgs e)
		    {
				masterPosition = null;
				if ( e.Position.Instrument == Instrument && e.Position.Account == MasterAccount )
					masterPosition = e.Position;	
			}
			//---		
		private void OnOrderUpdate(object sender, OrderEventArgs e)
	    	{
				if (!IsCopyAllowed) return;
				
				// ----------   ENRTY LONG/SHORT   -----------------------
				if (e.Order.Account == MasterAccount && e.Order.Instrument == Instrument)
					{
						if (e.OrderState == OrderState.Submitted && e.Order.IsMarket && e.OrderState != OrderState.CancelSubmitted )
							{
								if (Acc1 != null ) sendOrder(Acc1, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc2 != null ) sendOrder(Acc2, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc3 != null ) sendOrder(Acc3, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc4 != null ) sendOrder(Acc4, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc5 != null ) sendOrder(Acc5, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								
								if (Acc6 != null ) sendOrder(Acc6, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc7 != null ) sendOrder(Acc7, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc8 != null ) sendOrder(Acc8, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc9 != null ) sendOrder(Acc9, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc10 != null ) sendOrder(Acc10, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								
								if (Acc11 != null ) sendOrder(Acc11, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc12 != null ) sendOrder(Acc12, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc13 != null ) sendOrder(Acc13, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc14 != null ) sendOrder(Acc14, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc15 != null ) sendOrder(Acc15, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								
								if (Acc16 != null ) sendOrder(Acc16, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc17 != null ) sendOrder(Acc17, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc18 != null ) sendOrder(Acc18, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc19 != null ) sendOrder(Acc19, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								if (Acc20 != null ) sendOrder(Acc20, e.Order.OrderAction, e.Order.OrderType, e.Order.Quantity, e.Order.OrderId );
								
							}
					}
					//---
				if (e.Order.Account == MasterAccount && e.Order.Instrument == Instrument)
					{
						if ( e.OrderState == OrderState.Filled && ( e.Order.IsLimit || e.Order.IsStopMarket ) )
							{
								if (Acc1 != null ) sendOrder(Acc1, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc2 != null ) sendOrder(Acc2, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc3 != null ) sendOrder(Acc3, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc4 != null ) sendOrder(Acc4, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc5 != null ) sendOrder(Acc5, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								
								if (Acc6 != null ) sendOrder(Acc6, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc7 != null ) sendOrder(Acc7, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc8 != null ) sendOrder(Acc8, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc9 != null ) sendOrder(Acc9, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc10 != null ) sendOrder(Acc10, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								
								if (Acc11 != null ) sendOrder(Acc11, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc12 != null ) sendOrder(Acc12, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc13 != null ) sendOrder(Acc13, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc14 != null ) sendOrder(Acc14, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc15 != null ) sendOrder(Acc15, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								
								if (Acc16 != null ) sendOrder(Acc16, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc17 != null ) sendOrder(Acc17, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc18 != null ) sendOrder(Acc18, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc19 != null ) sendOrder(Acc19, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								if (Acc20 != null ) sendOrder(Acc20, e.Order.OrderAction, OrderType.Market, e.Order.Quantity, e.Order.OrderId );
								
							}
					}
	    }
		//---
		private void sendOrder(Account acc, OrderAction ordAction, OrderType ordType, int ordQuantity, string ordName)
			{
				if (!IsCopyAllowed) return;
				Order ordToSend = null;
				ordToSend = acc.CreateOrder(Instrument, ordAction, ordType, OrderEntry.Manual, TimeInForce.Day, ordQuantity ,0, 0, "", ordName, DateTime.MaxValue, null);
				acc.Submit(new[] { ordToSend });
			}
			//---
		
		protected override void OnBarUpdate()
		{
			//Add your custom indicator logic here.
			
		}
		
		#region properties
		
		[NinjaScriptProperty]
		[XmlIgnore]
		[Display(Name="Support/HowToUse/SetUp -> :",  		       					Order=0, GroupName="0. Support")]
		public string ThisTool
		{ get; set; }
		
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="Copy from this Account ", 								Order=1, GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string MasterAccount_name { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 1", 											Order=2, GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_1 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 2", 											Order=3,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_2 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 3", 											Order=4,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_3 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 4", 											Order=5,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_4 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 5", 											Order=6,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_5 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 6", 											Order=7,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_6 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 7", 											Order=8,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_7 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 8", 											Order=9,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_8 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 9", 											Order=10,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_9 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 10", 											Order=11,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_10 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 11", 											Order=12,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_11 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 12", 											Order=13,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_12 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 13", 											Order=14,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_13 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 14", 											Order=15,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_14 { get; set; }

		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 15", 											Order=16,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_15 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 16", 											Order=17,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_16 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 17", 											Order=18,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_17 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 18", 											Order=19,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_18 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 19", 											Order=20,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_19 { get; set; }
		
		[NinjaScriptProperty]//[XmlIgnore]
		[Display(Name="To Account 20", 											Order=21,  GroupName="1. Replicate ChartTrader")]
		[TypeConverter(typeof(NinjaTrader.NinjaScript.AccountNameConverter))]
		public string Accountname_20 { get; set; }
		
		#endregion
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private Toasthedge.CopyCat[] cacheCopyCat;
		public Toasthedge.CopyCat CopyCat(string thisTool, string masterAccount_name, string accountname_1, string accountname_2, string accountname_3, string accountname_4, string accountname_5, string accountname_6, string accountname_7, string accountname_8, string accountname_9, string accountname_10, string accountname_11, string accountname_12, string accountname_13, string accountname_14, string accountname_15, string accountname_16, string accountname_17, string accountname_18, string accountname_19, string accountname_20)
		{
			return CopyCat(Input, thisTool, masterAccount_name, accountname_1, accountname_2, accountname_3, accountname_4, accountname_5, accountname_6, accountname_7, accountname_8, accountname_9, accountname_10, accountname_11, accountname_12, accountname_13, accountname_14, accountname_15, accountname_16, accountname_17, accountname_18, accountname_19, accountname_20);
		}

		public Toasthedge.CopyCat CopyCat(ISeries<double> input, string thisTool, string masterAccount_name, string accountname_1, string accountname_2, string accountname_3, string accountname_4, string accountname_5, string accountname_6, string accountname_7, string accountname_8, string accountname_9, string accountname_10, string accountname_11, string accountname_12, string accountname_13, string accountname_14, string accountname_15, string accountname_16, string accountname_17, string accountname_18, string accountname_19, string accountname_20)
		{
			if (cacheCopyCat != null)
				for (int idx = 0; idx < cacheCopyCat.Length; idx++)
					if (cacheCopyCat[idx] != null && cacheCopyCat[idx].ThisTool == thisTool && cacheCopyCat[idx].MasterAccount_name == masterAccount_name && cacheCopyCat[idx].Accountname_1 == accountname_1 && cacheCopyCat[idx].Accountname_2 == accountname_2 && cacheCopyCat[idx].Accountname_3 == accountname_3 && cacheCopyCat[idx].Accountname_4 == accountname_4 && cacheCopyCat[idx].Accountname_5 == accountname_5 && cacheCopyCat[idx].Accountname_6 == accountname_6 && cacheCopyCat[idx].Accountname_7 == accountname_7 && cacheCopyCat[idx].Accountname_8 == accountname_8 && cacheCopyCat[idx].Accountname_9 == accountname_9 && cacheCopyCat[idx].Accountname_10 == accountname_10 && cacheCopyCat[idx].Accountname_11 == accountname_11 && cacheCopyCat[idx].Accountname_12 == accountname_12 && cacheCopyCat[idx].Accountname_13 == accountname_13 && cacheCopyCat[idx].Accountname_14 == accountname_14 && cacheCopyCat[idx].Accountname_15 == accountname_15 && cacheCopyCat[idx].Accountname_16 == accountname_16 && cacheCopyCat[idx].Accountname_17 == accountname_17 && cacheCopyCat[idx].Accountname_18 == accountname_18 && cacheCopyCat[idx].Accountname_19 == accountname_19 && cacheCopyCat[idx].Accountname_20 == accountname_20 && cacheCopyCat[idx].EqualsInput(input))
						return cacheCopyCat[idx];
			return CacheIndicator<Toasthedge.CopyCat>(new Toasthedge.CopyCat(){ ThisTool = thisTool, MasterAccount_name = masterAccount_name, Accountname_1 = accountname_1, Accountname_2 = accountname_2, Accountname_3 = accountname_3, Accountname_4 = accountname_4, Accountname_5 = accountname_5, Accountname_6 = accountname_6, Accountname_7 = accountname_7, Accountname_8 = accountname_8, Accountname_9 = accountname_9, Accountname_10 = accountname_10, Accountname_11 = accountname_11, Accountname_12 = accountname_12, Accountname_13 = accountname_13, Accountname_14 = accountname_14, Accountname_15 = accountname_15, Accountname_16 = accountname_16, Accountname_17 = accountname_17, Accountname_18 = accountname_18, Accountname_19 = accountname_19, Accountname_20 = accountname_20 }, input, ref cacheCopyCat);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.Toasthedge.CopyCat CopyCat(string thisTool, string masterAccount_name, string accountname_1, string accountname_2, string accountname_3, string accountname_4, string accountname_5, string accountname_6, string accountname_7, string accountname_8, string accountname_9, string accountname_10, string accountname_11, string accountname_12, string accountname_13, string accountname_14, string accountname_15, string accountname_16, string accountname_17, string accountname_18, string accountname_19, string accountname_20)
		{
			return indicator.CopyCat(Input, thisTool, masterAccount_name, accountname_1, accountname_2, accountname_3, accountname_4, accountname_5, accountname_6, accountname_7, accountname_8, accountname_9, accountname_10, accountname_11, accountname_12, accountname_13, accountname_14, accountname_15, accountname_16, accountname_17, accountname_18, accountname_19, accountname_20);
		}

		public Indicators.Toasthedge.CopyCat CopyCat(ISeries<double> input , string thisTool, string masterAccount_name, string accountname_1, string accountname_2, string accountname_3, string accountname_4, string accountname_5, string accountname_6, string accountname_7, string accountname_8, string accountname_9, string accountname_10, string accountname_11, string accountname_12, string accountname_13, string accountname_14, string accountname_15, string accountname_16, string accountname_17, string accountname_18, string accountname_19, string accountname_20)
		{
			return indicator.CopyCat(input, thisTool, masterAccount_name, accountname_1, accountname_2, accountname_3, accountname_4, accountname_5, accountname_6, accountname_7, accountname_8, accountname_9, accountname_10, accountname_11, accountname_12, accountname_13, accountname_14, accountname_15, accountname_16, accountname_17, accountname_18, accountname_19, accountname_20);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.Toasthedge.CopyCat CopyCat(string thisTool, string masterAccount_name, string accountname_1, string accountname_2, string accountname_3, string accountname_4, string accountname_5, string accountname_6, string accountname_7, string accountname_8, string accountname_9, string accountname_10, string accountname_11, string accountname_12, string accountname_13, string accountname_14, string accountname_15, string accountname_16, string accountname_17, string accountname_18, string accountname_19, string accountname_20)
		{
			return indicator.CopyCat(Input, thisTool, masterAccount_name, accountname_1, accountname_2, accountname_3, accountname_4, accountname_5, accountname_6, accountname_7, accountname_8, accountname_9, accountname_10, accountname_11, accountname_12, accountname_13, accountname_14, accountname_15, accountname_16, accountname_17, accountname_18, accountname_19, accountname_20);
		}

		public Indicators.Toasthedge.CopyCat CopyCat(ISeries<double> input , string thisTool, string masterAccount_name, string accountname_1, string accountname_2, string accountname_3, string accountname_4, string accountname_5, string accountname_6, string accountname_7, string accountname_8, string accountname_9, string accountname_10, string accountname_11, string accountname_12, string accountname_13, string accountname_14, string accountname_15, string accountname_16, string accountname_17, string accountname_18, string accountname_19, string accountname_20)
		{
			return indicator.CopyCat(input, thisTool, masterAccount_name, accountname_1, accountname_2, accountname_3, accountname_4, accountname_5, accountname_6, accountname_7, accountname_8, accountname_9, accountname_10, accountname_11, accountname_12, accountname_13, accountname_14, accountname_15, accountname_16, accountname_17, accountname_18, accountname_19, accountname_20);
		}
	}
}

#endregion
