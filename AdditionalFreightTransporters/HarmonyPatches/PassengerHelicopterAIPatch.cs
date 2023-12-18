using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace AdditionalFreightTransporters.HarmonyPatches
{
    [HarmonyPatch(typeof(PassengerHelicopterAI))]
    internal static class PassengerHelicopterAIPatch
    {
        [HarmonyPatch(typeof(PassengerHelicopterAI), "StartPathFind",
                [typeof(ushort), typeof(Vehicle), typeof(Vector3), typeof(Vector3), typeof(bool), typeof(bool), typeof(bool)],
                [ArgumentType.Normal, ArgumentType.Ref, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal])]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpile(MethodBase original,
            IEnumerable<CodeInstruction> instructions)
        {
            Debug.Log("AdditionalFreightTransporters: Transpiling method: " + original.DeclaringType + "." + original);
            var codes = new List<CodeInstruction>(instructions);
            var newCodes = new List<CodeInstruction>();
            foreach (var codeInstruction in codes)
            {
                if (SkipInstruction(codeInstruction))
                {
                    newCodes.Add(codeInstruction);
                    continue;
                }

                var newInstruction = codeInstruction.operand.Equals(65540)
                        ? new CodeInstruction(OpCodes.Ldc_I4,
                        (int)(Vehicle.Flags.Spawned | Vehicle.Flags.WaitingPath | Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo))
                        {
                            labels = codeInstruction.labels
                        }
                        : new CodeInstruction(OpCodes.Ldc_I4, (int)byte.MaxValue)
                        {
                            labels = codeInstruction.labels
                        }
                    ;
                newCodes.Add(newInstruction);
                Debug.LogWarning($"CargoHelicopters: Replaced vehicle flags with {newInstruction.operand}");
            }

            return newCodes.AsEnumerable();
        }

        private static bool SkipInstruction(CodeInstruction codeInstruction)
        {
            return codeInstruction.opcode != OpCodes.Ldc_I4 || codeInstruction.operand == null ||
                   (!65796.Equals(codeInstruction.operand) && !150.Equals(codeInstruction.operand));
        }
    }
}
