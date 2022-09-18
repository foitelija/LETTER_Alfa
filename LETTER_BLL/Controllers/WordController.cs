using LETTER_BLL.Interfaces;
using LETTER_DAL.Models;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LETTER_BLL.Controllers
{
    public class WordController : IWordController
    {
        string[] constLines = File.ReadLines(ConstFiles.ConstClients).ToArray();

        public List<Clients> wordClients; // основной лист
        public List<Clients> wordClientsDTO; // передаёт этих клиентов

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
                            await CheckTheSame();
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

        public async Task CheckTheSame()
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

        public Task StartupWord(int choose)
        {
            throw new NotImplementedException();
        }
    }
}
