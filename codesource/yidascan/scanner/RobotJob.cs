using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yidascan.scanner {
    public class RobotJobQueue {
        public Queue<RollPosition> Rolls { get; set; }

        public void AddRoll(RollPosition roll) {
            lock (Rolls) {
                Rolls.Enqueue(roll);
            }
        }

        public RollPosition GetRoll() {
            lock (Rolls) {
                return Rolls.Count == 0 ?
                    null : Rolls.Dequeue();
            }
        }

        public void ResetRolls() {
            lock (Rolls) {
                while (Rolls.Count > 0) {
                    Rolls.Dequeue();
                }
            }
        }

    }
}
