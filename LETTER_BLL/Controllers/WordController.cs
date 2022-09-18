using LETTER_BLL.Interfaces;
using LETTER_DAL.Models;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using word = Microsoft.Office.Interop.Word;

namespace LETTER_BLL.Controllers
{
    public class WordController : IWordController
    {
        #region FIELD
        string clientDocument = string.Empty;
        string generalContractDocument = string.Empty;
        string monthDocument = string.Empty;
        string dateCloseDocument = string.Empty;
        string dateOpenDocument = string.Empty;
        string currencyDocument = string.Empty;
        string amountDocument = string.Empty;
        #endregion



        #region ARRAY, CLASS AND LISTS
        string[] constLines = File.ReadLines(ConstFiles.ConstClients).ToArray();
        public List<Clients> wordClients; // основной лист.
        public List<Clients> wordClientsDTO; // передаёт этих клиентов.
        Clients _clients = new Clients(); // для вызова методов класса клиенты.
        #endregion

        public async Task SortingClients(List<Clients> clients)
        {
            wordClients = clients;

            await Task.Run(async () =>
            {
                if (wordClients.Count > 0)
                {
                    int IdenticalData = 0;
                    for (int i = 0; i < wordClients.Count; i++)
                    {
                        if (wordClients[0].Id == wordClients[i].Id)
                        {
                            IdenticalData++; // or == i;
                        }
                        if (wordClients[0].Id != wordClients[i].Id || (i + 1) == wordClients.Count)
                        {
                            wordClientsDTO = wordClients.GetRange(0, IdenticalData);
                            wordClients.RemoveRange(0, IdenticalData);
                            await CheckAutodebet();
                            break;
                        }
                    }
                }
                else
                {
                    //mailController

                }
            });
        }

        public async Task CheckAutodebet()
        {
            int WriteOffOption = 0; //вариант списания 1) автоматическое 2) не автоматическое
            for (int i = 0; i < wordClientsDTO.Count; i++)
            {
                if (constLines.Contains(wordClientsDTO[i].Id))
                {
                    WriteOffOption = 1;
                }
                else
                {
                    WriteOffOption = 2;
                }
            }
            await StartupWord(WriteOffOption);
            await SortingClients(wordClients);
        }

        public async Task StartupWord(int choose)
        {
            await Task.Delay(50);

            string fileExportName = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}_{DateTime.Now.ToString("HH.mm.ss")}_" + $"{wordClientsDTO[0].Id}" + "_готов";
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "шаблон.docx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Готовые" + "\\" + fileExportName + ".docx");

            object doc = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Готовые" + "\\" + fileExportName + ".docx";

            var wordapp = new word.Application();
            wordapp.Visible = false;
            object missing = Type.Missing;
            object falseValue = false;

            try
            {
                var wordDocument = wordapp.Documents.Open(doc, missing, falseValue, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                foreach (var items in wordClientsDTO)
                {
                    clientDocument = items.ClientName;
                    generalContractDocument = items.GeneralContract;
                    monthDocument = items.Month;
                    dateCloseDocument = items.DateEnd;
                    currencyDocument = items.Currency;
                }

                ReplaceWordStub("{КЛИЕНТ}", clientDocument, ref wordDocument);
                ReplaceWordStub("{ГЕНДОГ}", generalContractDocument, ref wordDocument);
                ReplaceWordStub("{МЕСЯЦ}", monthDocument, ref wordDocument);
                ReplaceWordStub("{ГОД}", DateTime.Now.Year.ToString(), ref wordDocument);

                for(int i = 0; i < wordClientsDTO.Count; i++)
                {
                    word.Table table = wordapp.ActiveDocument.Tables[1]; // change 1 -> 2
                    table.Rows.Add(ref missing);

                    table.Rows[i + 2].Cells[1].Range.Text = wordClientsDTO[i].ContractNumber;

                    #region ТАБЛИЦА
                    if (wordClientsDTO[i].CharReward.ToLower().Contains("дополнительное вознаграждение"))
                    {
                        amountDocument = wordClientsDTO[i].CurrReward = " " + wordClientsDTO[i].AmountToPay.ToString("0.00");
                        table.Rows[i + 2].Cells[2].Range.Text = wordClientsDTO[i].CurrReward + $" за период с {wordClientsDTO[i].DateStart.Replace("/", ".")} по {wordClientsDTO[i].DateEnd.Replace("/", ".")} по ставке {wordClientsDTO[i].Rate} годовых по банковской гарантии на сумму {amountDocument} {wordClientsDTO[i].Currency}";
                    }
                    else
                    {
                        table.Rows[i + 2].Cells[2].Range.Text = wordClientsDTO[i].CharReward;
                    }
                    table.Rows[i + 2].Cells[3].Range.Text = wordClientsDTO[i].CurrReward + " " + wordClientsDTO[i].ToPayment.ToString("0.00");

                    #endregion
                }

                //ReplaceWordStub("{ДАТАУПЛАТЫ}", _clients.TotalNumberOfDaysInMonth(DateTime.Now.Month) + dateCloseDocument.Remove(0,2).Replace("/","."),ref wordDocument);

                wordapp.ActiveDocument.SaveAs2();
                wordapp.Documents.Close();
                wordapp.Quit();
            }
            catch
            {

            }
        }

        private static void ReplaceWordStub(string stubToReplace, string text, ref word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }
    }
}
