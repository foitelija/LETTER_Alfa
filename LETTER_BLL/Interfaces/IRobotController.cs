using LETTER_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LETTER_BLL.Interfaces
{
    public interface IRobotController
    {
        Task<List<Clients>> RobotStartReadFile(string value);
        Task RobotStartWork();
    }
}
