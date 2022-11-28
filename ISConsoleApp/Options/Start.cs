using IS.Domain.DomainModels;
using ISConsoleApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISConsoleApp.Options
{
    public class Start
    {
        private readonly ITable<RobotModel, MovementModel> _table;
        RobotModel robot;
        public Start(ITable<RobotModel, MovementModel> table)
        {
            _table = table;
            robot = new RobotModel();
        }

        public void TableOptions(TableOptions opts)
        {
            // Known issue with not reading arguments from launchproperties so have hard coded.
            opts.TableItem = 1;
            opts.FilePath = "commands.json";

            Console.WriteLine("Welcome. Choose table item...");
            Console.WriteLine("-----------------------------");

            Action<TableOptions> process = opts.TableItem switch
            {
                1 => (opts) => {
                    var commands = GetMovementCommands(opts);

                    foreach (var command in commands) {
                        _table.DoSomethingToTableItem(robot, command);
                    }
                 }
                ,
                _ => (opts) => { return; }
            };

            process.Invoke(opts);
        }


        private static IEnumerable<MovementModel> GetMovementCommands(TableOptions opts)
        {
            string body;

            Console.WriteLine($"Command: {opts.TableItem} FilePath {opts.FilePath}");

            // try file path
            if (!opts.FilePath.Equals(string.Empty) && File.Exists(opts.FilePath))
            {
                body = File.ReadAllText(opts.FilePath);
            }
            else
            {
                Console.Write("Could not find body");
                throw new Exception("Empty file not allowed");
            }

            Console.WriteLine($"Body: {body}");

            var commands = JsonConvert.DeserializeObject<MovementModel[]>(body);
            Console.WriteLine($"Found {commands.Count()} commands");
            return commands;
        }
    }
}
