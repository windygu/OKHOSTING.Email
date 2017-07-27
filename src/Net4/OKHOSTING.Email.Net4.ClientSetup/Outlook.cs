using System;
using System.Collections.Generic;
using System.Diagnostics;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using System.Threading;

namespace OKHOSTING.Email.Net4.ClientSetup
{
	/// <summary>
	/// Automatize to create account profile in outlook.
	/// </summary>
	public static class Outlook
	{
		/// <summary>
		/// Setups a mail account in outlook 2007/ 2010 /2013
		/// </summary>
		/// <param name="name">Name of the account holder</param>
		/// <param name="mailAddress">Mail account to setup</param>
		/// <param name="password">Password of the mail account</param>
		/// <param name="deleteIfExists">If set to true and the account already exists, deletes it and creates it from scratch, otherwise, edit the existing account</param>
		public static void SetupMailAccount(string name, string mailAddress, string password, bool deleteIfExists, string protocol)
		{
			List<String> ports = new List<String>();
			ports.Add("25");
			ports.Add("465");
			ports.Add("587");

			//Found Aplication exists.
			try { Type outlookType = Type.GetTypeFromProgID("Outlook.Application", true); }
			catch (SystemException) { return; }

			Application control = TestStack.White.Application.Launch(new ProcessStartInfo("control.exe"));
			Process[] processes = System.Diagnostics.Process.GetProcesses();
			Window paneControl = null;

			foreach (Process process in processes)
			{
				if (process.ProcessName.ToString() == "explorer")
				{
					Application explorer = Application.Attach(process);
					paneControl = explorer.GetWindow("Panel de control");
					break;
				}
			}

			paneControl.Get<TestStack.White.UIItems.Hyperlink>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Cuentas de usuario y protección infantil")).Click();
			paneControl.Get<TestStack.White.UIItems.Hyperlink>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Correo")).Click();
			
			Window window = null;
			while (window == null)
			{
				window = Desktop.Instance.Windows().Find(obj => obj.Title.Contains("Configuración de correo - Outlook"));
				System.Threading.Thread.Sleep(500);
			}

			var button = ClickButton("Cuentas de correo electrónico...", window);

			var dataGrip = window.Get<TestStack.White.UIItems.ListView>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("AcctList"));
			int index = -1;
			for (int i = 0; i < dataGrip.Items.Count; i++)
			{
				if (dataGrip.Items[i].ToString() == mailAddress)
				{
					index = i;
					continue;
				}
			}

			if (index > -1)
			{
				//Focus in Object Account.
				dataGrip.Cell("Nombre", index).Click();
				
				// Find the email into  the listView for send message of error.
				if(deleteIfExists)
				{
						DeleteAccount(window);
						CreateAccount(name, mailAddress, password, window, ports, protocol);
				}
				else
				{
					//EditAccount(window, name, mailAddress, password);
					ClickButton("Cerrar", window);
					ClickButton("Cerrar", window);
					return;
				}
			}
			else
			{
				CreateAccount(name, mailAddress, password, window, ports, protocol);
			}
		}

		private static void CreateAccount(string name, string mailAddress, string password, Window window, List<string> ports, string protocol)
		{
			ClickButton("Nuevo...", window);
			ClickButton("Siguiente >", window);

			if (protocol == "POP")
			{
				AccountPOP(name, mailAddress, password, window, ports);
			}
			else
			{
				AccountIMAP(name, mailAddress, password, window);
			}
		}

		private static void AccountPOP(string name, string mailAddress, string password, Window window, List<string> ports)
		{
			string mailHost = "mail." + mailAddress.Split('@')[1];
			ClickRadioButton(window, "Configura");
			ClickButton("Siguiente >", window);
			ClickRadioButton(window, "POP o IMAP");
			ClickButton("Siguiente >", window);
			InsertText(name, "Su nombre:", window);
			InsertText(mailAddress, "Dirección de correo electrónico", window);
			InsertText(mailHost, "Servidor de correo entrante:", window);
			InsertText(mailHost, "Servidor de correo saliente (SMTP):", window);
			InsertText(mailAddress, "Nombre de usuario:", window);
			InsertText(password, "Contraseña:", window);
			ClickButton("Más configuraciones ...", window);
			var tabMenu = ClickTap(window, "12320", 1);				
			tabMenu.SelectTabPage(1);
			ClickCheckButton(window, "Mi servidor de salida (SMTP) requiere autenticación");
			try
			{
				tabMenu.SelectTabPage(3);
			}
			catch (Exception e)
			{
				tabMenu.SelectTabPage(2);
			}
			InsertText(ports[0], "Servidor de salida (SMTP):", window);
			ports.Remove(ports[0]);
			ClickButton("Aceptar", window);
			ClickButton("Probar configuración de la cuenta ...", window);
			ClickTap(window, "461", 0);
			
			while (true)
			{
				TestStack.White.UIItems.ListView list = GetListView("460", window);
				String loginSession = StatusAccount(list, 0, window);

				if (loginSession == "")
				{
					ClickButton("Cancelar", window);
				}

				String testMessage = StatusAccount(list, 1, window);
				if (testMessage == "Error" && ports.Count > 0)
				{
					ClickButton("Cerrar", window);
					ClickButton("Más configuraciones ...", window);					
					ClickTap(window, "12320", 3);
					InsertText(ports[0], "Servidor de salida (SMTP):", window);
					ports.Remove(ports[0]);
					ClickButton("Aceptar", window);
					ClickButton("Probar configuración de la cuenta ...", window);
				}
				else
				{
					if (testMessage != "Completado" && ports.Count > 0 && testMessage != "")
						System.Threading.Thread.Sleep(1000);
					else
					{
						if (testMessage == "")
						{
							ClickButton("Cancelar", window);
							ClickButton("Cerrar", window);
							ClickButton("Cancelar", window);
							ClickButton("Cerrar", window);
							ClickButton("Cerrar", window);
							break;
						}
						else
						{
							ClickButton("Cerrar", window);
							if (testMessage == "Error")
							{
								ClickButton("Cancelar", window);
							}
							break;
						}
					}
				}					
			}

			ClickButton("Siguiente >", window);
			ClickButton("Cerrar", window);
			ClickButton("Finalizar", window);
			ClickButton("Cerrar", window);
			ClickButton("Cerrar", window);
		}

		private static void AccountIMAP(string name, string mailAddress, string password, Window window)
		{
			InsertText(name, "Su nombre:", window);
			InsertText(mailAddress, "Dirección de correo electrónico:", window);
			InsertText(password, "Contraseña:", window);
			InsertText(password, "Repita la contraseña:", window);			
			ClickWaitButton("Siguiente >", window);
			ClickWaitButton("Siguiente >", window);

			try
			{
				window.Get<TestStack.White.UIItems.Button>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Reintentar"));
				ClickButton("Cancelar", window);
			}
			catch (Exception e) { }

			ClickButton("Finalizar", window);
			ClickButton("Cerrar", window);
			ClickButton("Cerrar", window);
		}

		private static TestStack.White.UIItems.ListView GetListView(string idListView, Window window)
		{
			TestStack.White.UIItems.ListView listView = null;
			try
			{
				listView = window.Get<TestStack.White.UIItems.ListView>(TestStack.White.UIItems.Finders.SearchCriteria.ByAutomationId(idListView));
			}
			catch (Exception) { }

			return listView;
		}

		private static string StatusAccount(TestStack.White.UIItems.ListView listTask, int numCell, Window window)
		{
			string statusAccount = "";
			while (true)
			{
				try
				{
					if (listTask.Cell("Estado", numCell).Name == "En curso")
					{											
						var check = ClickCheckButton(window, "Guardar contraseña en su lista de contraseñas");
						if (check != null)
						{
							break;
						}
						System.Threading.Thread.Sleep(1000);
					}
					else
					{
						statusAccount = listTask.Cell("Estado", numCell).Name;
						break;
					}
				}
				catch (Exception)
				{
					ClickTap(window, "461", 0);
				}
				
			}
			return statusAccount;
		}

		private static void ClickRadioButton(Window window, string name)
		{
			try
			{
				foreach (var item in window.Items)
				{
					if(item.Name.Contains(name))
					{
						item.Click();
						window.WaitWhileBusy();
						System.Threading.Thread.Sleep(1000);
						break;
					}
				}
				/*
				var radioButton = window.Get<TestStack.White.UIItems.RadioButton>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Configurar manualmente las opciones del servidor o tipos de servidores adicionales"));
				radioButton.Click();
				window.WaitWhileBusy();
				System.Threading.Thread.Sleep(1000);*/
			}
			catch (Exception e) { }
		}

		private static TestStack.White.UIItems.CheckBox ClickCheckButton(Window window, string nameBnt)
		{
			TestStack.White.UIItems.CheckBox checkButton =  null;
			try
			{
				checkButton = window.Get<TestStack.White.UIItems.CheckBox>(TestStack.White.UIItems.Finders.SearchCriteria.ByText(nameBnt));
				checkButton.Click();
				window.WaitWhileBusy();
				System.Threading.Thread.Sleep(1000);
			}
			catch (Exception e) { }
			return checkButton;
		}

		private static TestStack.White.UIItems.TabItems.Tab ClickTap(Window window, string idTap, int numTap)
		{
			TestStack.White.UIItems.TabItems.Tab tabMenu = null;
			try 
			{
				tabMenu = window.Get<TestStack.White.UIItems.TabItems.Tab>(TestStack.White.UIItems.Finders.SearchCriteria.ByAutomationId(idTap));
				tabMenu.SelectTabPage(numTap);				
			}
			catch (Exception) {
				tabMenu.SelectTabPage(numTap-1);
			}
			return tabMenu;
		}

		private static void DeleteAccount(Window window)
		{
			ClickButton("Quitar", window);

			var language = Thread.CurrentThread.CurrentCulture.NativeName;
			if (language.ToString() == "English (United States)")
			{
				ClickButton("Yes", window);
			}
			else
			{
				ClickButton("Sí", window);
			}
		}

		private static void EditAccount(Window window, string name, string mailAddress, string password)
		{
			string mailHost = "mail." + mailAddress.Split('@')[1];
			
			ClickButton("Cambiar...", window);
			InsertText(name, "Su nombre:", window);
			InsertText(mailAddress, "Dirección de correo electrónico:", window);
			InsertText(mailHost, "Servidor de correo entrante:", window);
			InsertText(mailHost, "Servidor de correo saliente (SMTP):", window);
			InsertText(mailAddress, "Nombre de usuario:", window);
			InsertText(password, "Contraseña:", window);

			ClickButton("Probar configuración de la cuenta ...", window);
			var tapPage = window.Get<TestStack.White.UIItems.TabItems.Tab>(TestStack.White.UIItems.Finders.SearchCriteria.ByAutomationId("461"));

			if (tapPage.SelectedTab.ToString() != "Tareas")
			{
				tapPage.SelectTabPage(0);
			}

			var dataGrip = window.Get<TestStack.White.UIItems.ListView>(TestStack.White.UIItems.Finders.SearchCriteria.ByAutomationId("460"));
			var flagButton = window.Get<TestStack.White.UIItems.Button>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Cerrar"));

			while (!flagButton.Enabled)
			{
				System.Threading.Thread.Sleep(2000);
				ClickButton("Cancelar", window);
			}

			ClickButton("Cerrar", window);
			ClickButton("Cancelar", window);
			ClickButton("Cerrar", window);
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
				}

				return button;
			}
			catch (Exception e)
			{
				return button;
			}
		}

		private static TestStack.White.UIItems.Button ClickButton(string nameButton, Window window)
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
				foreach (var item in window.Items)
				{
					if (item.Name.Contains(fild) && item.GetType().ToString() == "TestStack.White.UIItems.TextBox")
					{
						TestStack.White.UIItems.TextBox text = (TestStack.White.UIItems.TextBox)item;
						text.Text = "";
						text.Enter(addText);
						System.Threading.Thread.Sleep(1000);
					}
				}
				/*var text = window.Get<TestStack.White.UIItems.TextBox>(TestStack.White.UIItems.Finders.SearchCriteria.ByText(fild));
				text.Text = "";
				text.Enter(addText);
				System.Threading.Thread.Sleep(1000);*/
			}
			catch (Exception e) { }
		}

		private static void ExecuteCommand(string _Command)
		{
			//Indicamos que deseamos inicializar el proceso cmd.exe junto a un comando de arranque. 
			//(/C, le indicamos al proceso cmd que deseamos que cuando termine la tarea asignada se cierre el proceso).
			//Para mas informacion consulte la ayuda de la consola con cmd.exe /? 
			System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + _Command);
			// Indicamos que la salida del proceso se redireccione en un Stream
			procStartInfo.RedirectStandardOutput = true;
			procStartInfo.UseShellExecute = false;
			//Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
			procStartInfo.CreateNoWindow = false;
			//Inicializa el proceso
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo = procStartInfo;
			proc.Start();
			//Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
			string result = proc.StandardOutput.ReadToEnd();
			//Muestra en pantalla la salida del Comando
			Console.WriteLine(result);
		}
	}
}