using System;
using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

namespace AdditionalFreightTransporters.AI
{

    //based of CargoHarborAI but without animals, connections & checking height
    public class CargoFerryHarborAI : CargoStationAI
    {
        [NonSerialized] protected float m_quayOffset;

        public static bool IsCargoFerryHarbor(Building station)
        {
            if (station.m_flags == Building.Flags.None)
            {
                return false;
            }
            if (station.Info == null)
            {
                return false;
            }
            return station.Info.m_buildingAI is CargoFerryHarborAI;
        }


        public override void InitializePrefab()
        {
            base.InitializePrefab();
            float a = m_info.m_generatedInfo.m_max.z - 7f;
            if (m_info.m_paths != null)
            {
                for (int index1 = 0; index1 < m_info.m_paths.Length; ++index1)
                {
                    if (m_info.m_paths[index1].m_netInfo != null && m_info.m_paths[index1].m_netInfo.m_class.m_service == ItemClass.Service.Road && m_info.m_paths[index1].m_nodes != null)
                    {
                        for (int index2 = 0; index2 < m_info.m_paths[index1].m_nodes.Length; ++index2)
                        {
                            a = Mathf.Min(a, -16f - m_info.m_paths[index1].m_netInfo.m_halfWidth - m_info.m_paths[index1].m_nodes[index2].z);
                        }   
                    }
                }
            }
            m_quayOffset = a;
        }

        public override ToolBase.ToolErrors CheckBuildPosition(ushort relocateID, ref Vector3 position, ref float angle, float waterHeight, float elevation, ref Segment3 connectionSegment, out int productionRate, out int constructionCost)
        {
            ToolBase.ToolErrors toolErrors1 = ToolBase.ToolErrors.None;
            if (m_info.m_placementMode == BuildingInfo.PlacementMode.Shoreline && BuildingTool.SnapToCanal(position, out Vector3 pos, out Vector3 dir, out bool isQuay, 40f, false))
            {
                angle = Mathf.Atan2(dir.x, -dir.z);
                pos += dir * m_quayOffset;
                position.x = pos.x;
                position.z = pos.z;
                if (!isQuay)
                {
                    toolErrors1 |= ToolBase.ToolErrors.ShoreNotFound;
                } 
            }
            ToolBase.ToolErrors toolErrors2 = toolErrors1 | base.CheckBuildPosition(relocateID, ref position, ref angle, waterHeight, elevation, ref connectionSegment, out productionRate, out constructionCost);
            return toolErrors2;
        }

        public override bool GetWaterStructureCollisionRange(out float min, out float max)
        {
            if (m_info.m_placementMode != BuildingInfo.PlacementMode.Shoreline)
            {
                return base.GetWaterStructureCollisionRange(out min, out max);
            }            min = 20f / Mathf.Max(22f, m_info.m_cellLength * 8f);
            max = 1f;
            return true;
        }

        public override bool RequireRoadAccess() => true;


        public override void CalculateSpawnPosition(ushort buildingID, ref Building data, ref Randomizer randomizer, VehicleInfo info, out Vector3 position, out Vector3 target)
        {
            if (info.m_vehicleType == VehicleInfo.VehicleType.Car)
            {
                if (info.m_class.m_service == ItemClass.Service.Industrial)
                {
                    if (InvertPositions(data))
                    {
                        position = data.CalculatePosition(m_truckUnspawnPosition);
                        target = data.CalculateSidewalkPosition(m_truckUnspawnPosition.x, 0.0f);
                    }
                    else
                    {
                        position = data.CalculatePosition(m_truckSpawnPosition);
                        target = data.CalculateSidewalkPosition(m_truckSpawnPosition.x, 0.0f);
                    }
                }
                else
                    BaseCalculateSpawnPosition(buildingID, ref data, ref randomizer, info, out position, out target);
            }
            else if (m_transportInfo != null && info.m_vehicleType == m_transportInfo.m_vehicleType)
            {
                position = data.CalculatePosition(m_spawnPosition);
                if (m_canInvertTarget && Singleton<SimulationManager>.instance.m_metaData.m_invertTraffic == SimulationMetaData.MetaBool.True)
                    target = data.CalculatePosition(m_spawnPosition * 2f - m_spawnTarget);
                else
                    target = data.CalculatePosition(m_spawnTarget);
            }
            else if (m_transportInfo2 != null && info.m_vehicleType == m_transportInfo2.m_vehicleType)
            {
                position = data.CalculatePosition(m_spawnPosition2);
                if (m_canInvertTarget && Singleton<SimulationManager>.instance.m_metaData.m_invertTraffic == SimulationMetaData.MetaBool.True)
                    target = data.CalculatePosition(m_spawnPosition2 * 2f - m_spawnTarget2);
                else
                    target = data.CalculatePosition(m_spawnTarget2);
            }
            else
                BaseCalculateSpawnPosition(buildingID, ref data, ref randomizer, info, out position, out target);
        }

        public override void CalculateUnspawnPosition(ushort buildingID, ref Building data, ref Randomizer randomizer, VehicleInfo info, out Vector3 position, out Vector3 target)
        {
            if (info.m_vehicleType == VehicleInfo.VehicleType.Car)
            {
                if (info.m_class.m_service == ItemClass.Service.Industrial)
                {
                    if (InvertPositions(data) /*|| index % 2 == 1*/)
                    {
                        position = data.CalculatePosition(m_truckSpawnPosition);
                        target = data.CalculateSidewalkPosition(m_truckSpawnPosition.x, 0.0f);
                    }
                    else
                    {
                        position = data.CalculatePosition(m_truckUnspawnPosition);
                        target = data.CalculateSidewalkPosition(m_truckUnspawnPosition.x, 0.0f);
                    }
                    //index ++;
                }
                else
                {
                    BaseCalculateSpawnPosition(buildingID, ref data, ref randomizer, info, out position, out target);
                }
            }
            else if (m_transportInfo != null && info.m_vehicleType == m_transportInfo.m_vehicleType)
            {

                position = data.CalculatePosition(m_spawnPosition);
                if (m_canInvertTarget &&
                    Singleton<SimulationManager>.instance.m_metaData.m_invertTraffic !=
                    SimulationMetaData.MetaBool.True)
                {
                    target = data.CalculatePosition(m_spawnPosition * 2f - m_spawnTarget);
                }
                else
                {
                    target = data.CalculatePosition(m_spawnTarget);
                }
            }
            else if (m_transportInfo2 != null && info.m_vehicleType == m_transportInfo2.m_vehicleType)
            {

                position = data.CalculatePosition(m_spawnPosition2);
                if (m_canInvertTarget &&
                    Singleton<SimulationManager>.instance.m_metaData.m_invertTraffic !=
                    SimulationMetaData.MetaBool.True)
                {
                    target = data.CalculatePosition(m_spawnPosition2 * 2f - m_spawnTarget2);
                }
                else
                {
                    target = data.CalculatePosition(m_spawnTarget2);
                }
            }
            else
            {
                BaseCalculateUnspawnPosition(buildingID, ref data, ref randomizer, info, out position, out target);
            }
        }

        private static bool InvertPositions(Building data)
        {
            return (Singleton<SimulationManager>.instance.m_metaData.m_invertTraffic == SimulationMetaData.MetaBool.True && !Configuration.IsStationInverted(data)) || Configuration.IsStationInverted(data);
        }


        //PlayerBuildingAI implementation
        private static void BaseCalculateSpawnPosition(ushort buildingID, ref Building data, ref Randomizer randomizer, VehicleInfo info, out Vector3 position, out Vector3 target)
        {
            position = data.CalculateSidewalkPosition(0.0f, 3f);
            target = position;
        }

        //PlayerBuildingAI implementation
        private static void BaseCalculateUnspawnPosition(ushort buildingID, ref Building data, ref Randomizer randomizer, VehicleInfo info, out Vector3 position, out Vector3 target)
        {
            position = data.CalculateSidewalkPosition(0.0f, 3f);
            target = position;
        }
    }
}