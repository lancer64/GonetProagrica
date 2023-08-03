using ProagricaChallenge.DatabaseLayer.Models;
using ProagricaChallenge.Exceptions;
using ProagricaChallenge.RepositoryLayer;
using ProagricaChallenge.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProagricaChallenge.ControllerLayer
{
    public class TvShowsController: Controller
    {
        private TvShowService _service;

        public TvShowsController(TvShowService service)
        {
            _service = service;
        }

        /*
         * Added the controller in order to enhance the mantainability, since having
         * all the logic of the Application in a single file increase the risk
         * of errors that could easily escalate and be hard to detect.
        */
        public void RunMenu()
        {
            bool execute = true;
            Console.WriteLine("Welcome.");

            while (execute)
            {
                try
                {
                    Console.Write("\n--Type a command: ");
                    string command = Console.ReadLine() ?? string.Empty; ;
                    int t;
                    if (Int32.TryParse(command, out t))
                    {
                        int commandID = Convert.ToInt32(command);
                        _service.UpdateTvShow(commandID);
                    }
                    else
                    {
                        switch (command)
                        {
                            case "list":
                                _service.ShowTvShows();
                                break;
                            case "favorites":
                                _service.ShowFavorites();
                                break;
                            case "quit":
                                Console.WriteLine("Goodbye.");
                                execute = false;
                                break;
                            default:
                                Console.WriteLine($"Command \"{command}\" does not exist.");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

    }
}
