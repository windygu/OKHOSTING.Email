using System;
using TestStack.White;
using System.Diagnostics;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.InputDevices;
using System.Threading;

namespace OKHOSTING.ERP.Hosting.Mail.Clients
{
	public class Thunderbird
	{
		/// <summary>
		/// Setups a mail account in Thunderbird
		/// </summary>
		/// <param name="name">Name of the account holder</param>
		/// <param name="mailAddress">Mail account to setup</param>
		/// <param name="password">Password of the mail account</param>
		/// <returns>Response of process</returns>
		public static string SetupMailAccount(string name, string mailAddress, string password)
		{					   
			Window window = null;
			Application application;

			try
			{
				application = Application.AttachOrLaunch(new ProcessStartInfo("thunderbird.exe"));
				window = application.GetWindows()[0];
				window.Focus(DisplayState.Maximized);
			}
			catch (Exception)
			{
				application = Application.Launch("thunderbird.exe");
				window = application.GetWindows()[0];
				window.Focus(DisplayState.Maximized);
			}
				
			// close windows  initials when thunderbird open  
			deleteWindows(application);

			// Create account 
			CreateAccount(name, mailAddress, password, window);

			return "La cuenta fue creada!";		   
		}

		private static void deleteWindows(Application application)
		{
			var windows = application.GetWindows();
			foreach (var item in windows)
			{
				if (item.Name == "Integración con el sistema" || item.Name == "Bienvenido a Thunderbird")
				{
					var language = Thread.CurrentThread.CurrentCulture.NativeName;
					if (language.ToString() == "English (United States)")
					{
						ClickButton("Close", item);
					}
					else
					{
						ClickButton("Cerrar", item);
					}

					deleteWindows(application);
					
				}
			}
		}

		public static TestStack.White.UIItems.Button ClickButton(string nameButton, Window window)
		{
			TestStack.White.UIItems.Button button = null;
			try
			{
				button = window.Get<TestStack.White.UIItems.Button>(TestStack.White.UIItems.Finders.SearchCriteria.ByText(nameButton));
				button.Click();
				window.WaitWhileBusy();
				System.Threading.Thread.Sleep(1000);
				return button;
			}
			catch (Exception e)
			{
				return button;
			}
		}

		private static void InsertText(string addText, string fild, Window window)
		{
			try
			{
				bool band = false;
				foreach (var item in window.Items)
				{
					if (item.Name.Contains(fild))
					{											
						item.Click();
						System.Threading.Thread.Sleep(1000);
						Keyboard.Instance.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
						Keyboard.Instance.Enter("a");
						Keyboard.Instance.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
						item.Enter(addText);
						band = true;
						//item.DoubleClick();
						break;
					}
				}
				if (!band)
				{
					string fildThunderbird = "";
					if (fild.Contains("nombre"))
					{
						fildThunderbird = "s";
					}
					else
					{
						if (fild.Contains("Dirección"))
						{
							fildThunderbird = "d";
						}
						else
						{
							fildThunderbird = "a";
						}
					}
					


					Keyboard.Instance.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.ALT);
					Keyboard.Instance.Enter(fildThunderbird);
					Keyboard.Instance.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.ALT);

					Keyboard.Instance.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
					Keyboard.Instance.Enter("a");
					Keyboard.Instance.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);

					Keyboard.Instance.Enter(addText);				 
				}
			}
			catch (Exception e)
			{ }
		}

		private static TestStack.White.UIItems.Button ClickWaitButton(string nameButton, Window window)
		{
			TestStack.White.UIItems.Button button = null;

			try
			{
				button = window.Get<TestStack.White.UIItems.Button>(TestStack.White.UIItems.Finders.SearchCriteria.ByText(nameButton));
				button.Click();
				window.WaitWhileBusy();
				System.Threading.Thread.Sleep(1000);

				while (!button.Enabled)
				{
					System.Threading.Thread.Sleep(1000);
					button = window.Get<TestStack.White.UIItems.Button>(TestStack.White.UIItems.Finders.SearchCriteria.ByText(nameButton));
					ClickButton("Confirmar excepción de seguridad", window);
				}

				return button;
			}
			catch (Exception e)
			{
				return button;
			}
		}

		private static void CreateAccount(string name, string mailAddress, string password, Window window)
		{
			window.Keyboard.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.F10);			
			var navBarraMenu = window.Get<TestStack.White.UIItems.WindowStripControls.ToolStrip>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Barra de menú"));
			var option = navBarraMenu.MenuItem("Herramientas").ChildMenus;

			foreach (var item in option)
			{
				if (item.Name.Contains("Configuración"))
				{
					item.Click();
				}
			}
			
			var button = window.Get<TestStack.White.UIItems.Button>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Operaciones sobre la cuenta"));
			button.Click();
			Keyboard.Instance.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
			Keyboard.Instance.Enter("a");
			Keyboard.Instance.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
			InsertText(name, "nombre:", window);
			InsertText(mailAddress, "Dirección", window);
			InsertText(password, "Contraseña", window);
			ClickWaitButton("Continuar", window);
			ClickWaitButton("Hecho", window);
			ClickButton("Aceptar", window);
		}

		private static void listBox(Window window)
		{
			TestStack.White.UIItems.ListBoxItems.ListBox list = null;
			foreach (var item in window.Items)
			{
				var name = item.Name;
				try{
				if (item.GetType().Name == "ListBox")
				{
					
					list = (TestStack.White.UIItems.ListBoxItems.ListBox)item;
					if (list.Name == "110")
					{
						//list.Click();

						foreach (var h in list.Items)
						{
							h.Click();
							Keyboard.Instance.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
							Keyboard.Instance.Enter("a");
							Keyboard.Instance.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.CONTROL);
							list.Enter("111");
							System.Threading.Thread.Sleep(1000);
						}
					}
				}
				}catch(Exception){}						
			}
		}

		private static void ClickRadioButton(string nameRadio, Window window)
		{
			try
			{
				var radioButton = window.Get<TestStack.White.UIItems.RadioButton>(TestStack.White.UIItems.Finders.SearchCriteria.ByText(nameRadio));
				radioButton.Click();
				window.WaitWhileBusy();
				System.Threading.Thread.Sleep(1000);
			}
			catch (Exception e) { }
		}
	}
}
