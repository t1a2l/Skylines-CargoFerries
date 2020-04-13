using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

namespace CargoFerries.AI
{
  public class CargoFerryAI : FerryAI
  {
    [CustomizableProperty("Cargo capacity")]
    public int m_cargoCapacity = 1;

    public override Color GetColor(
      ushort vehicleID,
      ref Vehicle data,
      InfoManager.InfoMode infoMode)
    {
      switch (infoMode)
      {
        case InfoManager.InfoMode.Transport:
          return Singleton<TransportManager>.instance.m_properties.m_transportColors[
            (int) TransportInfo.TransportType.Ship];
        case InfoManager.InfoMode.Connections:
          InfoManager.SubInfoMode currentSubMode = Singleton<InfoManager>.instance.CurrentSubMode;
          TransferManager.TransferReason transferType = (TransferManager.TransferReason) data.m_transferType;
          if (currentSubMode == InfoManager.SubInfoMode.Default && (data.m_flags & Vehicle.Flags.Importing) !=
            ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
              Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
              Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
              Vehicle.Flags.Arriving | Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
              Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo |
              Vehicle.Flags.GoingBack | Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
              Vehicle.Flags.Exporting | Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
              Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
              Vehicle.Flags.Underground | Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
              Vehicle.Flags.LeftHandDrive) && transferType != TransferManager.TransferReason.None)
            return Singleton<TransferManager>.instance.m_properties.m_resourceColors[(int) transferType];
          return currentSubMode == InfoManager.SubInfoMode.WaterPower && (data.m_flags & Vehicle.Flags.Exporting) !=
                 ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                   Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                   Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                   Vehicle.Flags.Leaving | Vehicle.Flags.Arriving | Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                   Vehicle.Flags.Flying | Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                   Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack | Vehicle.Flags.WaitingTarget |
                   Vehicle.Flags.Importing | Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                   Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel | Vehicle.Flags.WaitingLoading |
                   Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                   Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding | Vehicle.Flags.LeftHandDrive) &&
                 transferType != TransferManager.TransferReason.None
            ? Singleton<TransferManager>.instance.m_properties.m_resourceColors[(int) transferType]
            : Singleton<InfoManager>.instance.m_properties.m_neutralColor;
        case InfoManager.InfoMode.TrafficRoutes:
          if (Singleton<InfoManager>.instance.CurrentSubMode == InfoManager.SubInfoMode.Default)
          {
            InstanceID empty = InstanceID.Empty;
            empty.Vehicle = vehicleID;
            return Singleton<NetManager>.instance.PathVisualizer.IsPathVisible(empty)
              ? Singleton<InfoManager>.instance.m_properties.m_routeColors[3]
              : Singleton<InfoManager>.instance.m_properties.m_neutralColor;
          }

          break;
      }

      return base.GetColor(vehicleID, ref data, infoMode);
    }

    public override string GetLocalizedStatus(
      ushort vehicleID,
      ref Vehicle data,
      out InstanceID target)
    {
      if ((data.m_flags & Vehicle.Flags.WaitingCargo) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                           Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                           Vehicle.Flags.TransferToTarget |
                                                           Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                           Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                           Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                           Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                           Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                           Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                           Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                           Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                           Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                           Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                           Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                           Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                           Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                           Vehicle.Flags.LeftHandDrive))
      {
        target = InstanceID.Empty;
        return ColossalFramework.Globalization.Locale.Get("VEHICLE_STATUS_CARGOSHIP_LOADING");
      }

      if ((data.m_flags & Vehicle.Flags.GoingBack) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                        Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                        Vehicle.Flags.TransferToTarget |
                                                        Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                        Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                        Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                        Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                        Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                        Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                        Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                        Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                        Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                        Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                        Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                        Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                        Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                        Vehicle.Flags.LeftHandDrive))
      {
        target = InstanceID.Empty;
        return ColossalFramework.Globalization.Locale.Get("VEHICLE_STATUS_CARGOTRUCK_RETURN");
      }

      if (data.m_targetBuilding != (ushort) 0)
      {
        target = InstanceID.Empty;
        target.Building = data.m_targetBuilding;
        return ColossalFramework.Globalization.Locale.Get("VEHICLE_STATUS_CARGOSHIP_TRANSPORT");
      }

      target = InstanceID.Empty;
      return ColossalFramework.Globalization.Locale.Get("VEHICLE_STATUS_CONFUSED");
    }

    public override string GetLocalizedStatus(
      ushort parkedVehicleID,
      ref VehicleParked data,
      out InstanceID target)
    {
      target = InstanceID.Empty;
      return ColossalFramework.Globalization.Locale.Get("VEHICLE_STATUS_CARGOSHIP_LOADING");
    }

    public override void GetBufferStatus(
      ushort vehicleID,
      ref Vehicle data,
      out string localeKey,
      out int current,
      out int max)
    {
      localeKey = "Default";
      current = 0;
      max = this.m_cargoCapacity;
      VehicleManager instance = Singleton<VehicleManager>.instance;
      ushort num1 = data.m_firstCargo;
      int num2 = 0;
      while (num1 != (ushort) 0)
      {
        ++current;
        num1 = instance.m_vehicles.m_buffer[(int) num1].m_nextCargo;
        if (++num2 > 16384)
        {
          CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + System.Environment.StackTrace);
          break;
        }
      }

      if ((data.m_flags & Vehicle.Flags.DummyTraffic) == ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                           Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                           Vehicle.Flags.TransferToTarget |
                                                           Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                           Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                           Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                           Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                           Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                           Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                           Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                           Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                           Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                           Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                           Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                           Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                           Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                           Vehicle.Flags.LeftHandDrive))
        return;
      Randomizer randomizer = new Randomizer((int) vehicleID);
      current = randomizer.Int32(max >> 1, max);
    }

    public override void CreateVehicle(ushort vehicleID, ref Vehicle data)
    {
      base.CreateVehicle(vehicleID, ref data);
      UnityEngine.Debug.LogWarning("Create vehicle!");
      data.m_flags |= Vehicle.Flags.WaitingTarget;
      data.m_flags |= Vehicle.Flags.WaitingCargo;
      data.m_flags |= Vehicle.Flags.WaitingLoading;
      data.m_flags |= Vehicle.Flags.Stopped;
    }

    public override void ReleaseVehicle(ushort vehicleID, ref Vehicle data)
    {
      UnityEngine.Debug.LogWarning("Release vehicle!");
      this.RemoveSource(vehicleID, ref data);
      this.RemoveTarget(vehicleID, ref data);
      base.ReleaseVehicle(vehicleID, ref data);
    }

    public override void LoadVehicle(ushort vehicleID, ref Vehicle data)
    {
      base.LoadVehicle(vehicleID, ref data);
      if (data.m_sourceBuilding != (ushort) 0)
        Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) data.m_sourceBuilding]
          .AddOwnVehicle(vehicleID, ref data);
      if (data.m_targetBuilding == (ushort) 0)
        return;
      Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) data.m_targetBuilding]
        .AddGuestVehicle(vehicleID, ref data);
    }

    public override void SetSource(ushort vehicleID, ref Vehicle data, ushort sourceBuilding)
    {
      UnityEngine.Debug.LogWarning($"Set source : {sourceBuilding}!");
      this.RemoveSource(vehicleID, ref data);
      data.m_sourceBuilding = sourceBuilding;
      if (sourceBuilding == (ushort) 0)
        return;
      UnityEngine.Debug.LogWarning("Set source: unspawn!");
      data.Unspawn(vehicleID);
      BuildingManager instance = Singleton<BuildingManager>.instance;
      Vector3 position;
      Vector3 target;
      instance.m_buildings.m_buffer[(int) sourceBuilding].Info.m_buildingAI.CalculateSpawnPosition(sourceBuilding,
        ref instance.m_buildings.m_buffer[(int) sourceBuilding], ref Singleton<SimulationManager>.instance.m_randomizer,
        this.m_info, out position, out target);
      Quaternion rotation = Quaternion.identity;
      Vector3 forward = target - position;
      if ((double) forward.sqrMagnitude > 0.00999999977648258)
        rotation = Quaternion.LookRotation(forward);
      data.m_frame0 = new Vehicle.Frame(position, rotation);
      data.m_frame1 = data.m_frame0;
      data.m_frame2 = data.m_frame0;
      data.m_frame3 = data.m_frame0;
      data.m_targetPos0 = (Vector4) (position + Vector3.ClampMagnitude(target - position, 0.5f));
      data.m_targetPos0.w = 2f;
      data.m_targetPos1 = data.m_targetPos0;
      data.m_targetPos2 = data.m_targetPos0;
      data.m_targetPos3 = data.m_targetPos0;
      this.FrameDataUpdated(vehicleID, ref data, ref data.m_frame0);
      UnityEngine.Debug.LogWarning("Adding own vehicle!");
      Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) sourceBuilding].AddOwnVehicle(vehicleID, ref data);
      if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) sourceBuilding].m_flags &
           Building.Flags.IncomingOutgoing) == Building.Flags.None)
        return;
      if ((data.m_flags & Vehicle.Flags.TransferToTarget) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                               Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                               Vehicle.Flags.TransferToTarget |
                                                               Vehicle.Flags.TransferToSource |
                                                               Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                               Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                               Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                               Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                               Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                               Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo |
                                                               Vehicle.Flags.GoingBack | Vehicle.Flags.WaitingTarget |
                                                               Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                                               Vehicle.Flags.Parking | Vehicle.Flags.CustomName |
                                                               Vehicle.Flags.OnGravel | Vehicle.Flags.WaitingLoading |
                                                               Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                                                               Vehicle.Flags.Underground | Vehicle.Flags.Transition |
                                                               Vehicle.Flags.InsideBuilding |
                                                               Vehicle.Flags.LeftHandDrive))
      {
        data.m_flags |= Vehicle.Flags.Importing;
      }
      else
      {
        if ((data.m_flags & Vehicle.Flags.TransferToSource) == ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                 Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                 Vehicle.Flags.TransferToTarget |
                                                                 Vehicle.Flags.TransferToSource |
                                                                 Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                                 Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                                 Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                                 Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                                 Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                 Vehicle.Flags.WaitingSpace |
                                                                 Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                                 Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                                 Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                                 Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                                 Vehicle.Flags.WaitingLoading |
                                                                 Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                                                                 Vehicle.Flags.Underground | Vehicle.Flags.Transition |
                                                                 Vehicle.Flags.InsideBuilding |
                                                                 Vehicle.Flags.LeftHandDrive))
          return;
        data.m_flags |= Vehicle.Flags.Exporting;
      }
    }

    public override void SetTarget(ushort vehicleID, ref Vehicle data, ushort targetBuilding)
    {
      UnityEngine.Debug.LogWarning($"Set target : {targetBuilding}!");
      if ((int) targetBuilding != (int) data.m_targetBuilding)
      {
        this.RemoveTarget(vehicleID, ref data);
        data.m_targetBuilding = targetBuilding;
        data.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                        Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                        Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                        Vehicle.Flags.Leaving | Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                        Vehicle.Flags.TakingOff | Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                        Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                        Vehicle.Flags.Importing | Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                        Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel | Vehicle.Flags.WaitingLoading |
                        Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                        Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding | Vehicle.Flags.LeftHandDrive;
        data.m_waitCounter = (byte) 0;
        if (targetBuilding != (ushort) 0)
        {
          Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) targetBuilding]
            .AddGuestVehicle(vehicleID, ref data);
          if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) targetBuilding].m_flags &
               Building.Flags.IncomingOutgoing) != Building.Flags.None)
          {
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                     Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                     Vehicle.Flags.TransferToTarget |
                                                                     Vehicle.Flags.TransferToSource |
                                                                     Vehicle.Flags.Emergency1 |
                                                                     Vehicle.Flags.Emergency2 |
                                                                     Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                                     Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                                     Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                                     Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                     Vehicle.Flags.WaitingSpace |
                                                                     Vehicle.Flags.WaitingCargo |
                                                                     Vehicle.Flags.GoingBack |
                                                                     Vehicle.Flags.WaitingTarget |
                                                                     Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                                                     Vehicle.Flags.Parking | Vehicle.Flags.CustomName |
                                                                     Vehicle.Flags.OnGravel |
                                                                     Vehicle.Flags.WaitingLoading |
                                                                     Vehicle.Flags.Congestion |
                                                                     Vehicle.Flags.DummyTraffic |
                                                                     Vehicle.Flags.Underground |
                                                                     Vehicle.Flags.Transition |
                                                                     Vehicle.Flags.InsideBuilding |
                                                                     Vehicle.Flags.LeftHandDrive))
              data.m_flags |= Vehicle.Flags.Exporting;
            else if ((data.m_flags & Vehicle.Flags.TransferToSource) != ~(Vehicle.Flags.Created |
                                                                          Vehicle.Flags.Deleted |
                                                                          Vehicle.Flags.Spawned |
                                                                          Vehicle.Flags.Inverted |
                                                                          Vehicle.Flags.TransferToTarget |
                                                                          Vehicle.Flags.TransferToSource |
                                                                          Vehicle.Flags.Emergency1 |
                                                                          Vehicle.Flags.Emergency2 |
                                                                          Vehicle.Flags.WaitingPath |
                                                                          Vehicle.Flags.Stopped |
                                                                          Vehicle.Flags.Leaving |
                                                                          Vehicle.Flags.Arriving |
                                                                          Vehicle.Flags.Reversed |
                                                                          Vehicle.Flags.TakingOff |
                                                                          Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                          Vehicle.Flags.WaitingSpace |
                                                                          Vehicle.Flags.WaitingCargo |
                                                                          Vehicle.Flags.GoingBack |
                                                                          Vehicle.Flags.WaitingTarget |
                                                                          Vehicle.Flags.Importing |
                                                                          Vehicle.Flags.Exporting |
                                                                          Vehicle.Flags.Parking |
                                                                          Vehicle.Flags.CustomName |
                                                                          Vehicle.Flags.OnGravel |
                                                                          Vehicle.Flags.WaitingLoading |
                                                                          Vehicle.Flags.Congestion |
                                                                          Vehicle.Flags.DummyTraffic |
                                                                          Vehicle.Flags.Underground |
                                                                          Vehicle.Flags.Transition |
                                                                          Vehicle.Flags.InsideBuilding |
                                                                          Vehicle.Flags.LeftHandDrive))
              data.m_flags |= Vehicle.Flags.Importing;
          }
        }
        else
          data.m_flags |= Vehicle.Flags.GoingBack;
      }

      if ((data.m_flags & Vehicle.Flags.WaitingCargo) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                           Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                           Vehicle.Flags.TransferToTarget |
                                                           Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                           Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                           Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                           Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                           Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                           Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                           Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                           Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                           Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                           Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                           Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                           Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                           Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                           Vehicle.Flags.LeftHandDrive) ||
          this.StartPathFind(vehicleID, ref data))
        return;
      UnityEngine.Debug.LogWarning("Set target: unspawn!");
      data.Unspawn(vehicleID);
    }

    public override void StartTransfer(
      ushort vehicleID,
      ref Vehicle data,
      TransferManager.TransferReason reason,
      TransferManager.TransferOffer offer)
    {
      if (reason == (TransferManager.TransferReason) data.m_transferType)
      {
        ushort building = offer.Building;
        if (building == (ushort) 0)
          return;
        this.SetTarget(vehicleID, ref data, building);
      }
      else
        base.StartTransfer(vehicleID, ref data, reason, offer);
    }

    public override void BuildingRelocated(ushort vehicleID, ref Vehicle data, ushort building)
    {
      base.BuildingRelocated(vehicleID, ref data, building);
      if ((int) building == (int) data.m_sourceBuilding)
      {
        if ((data.m_flags & Vehicle.Flags.GoingBack) == ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                          Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                          Vehicle.Flags.TransferToTarget |
                                                          Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                          Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                          Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                          Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                          Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                          Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                          Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                          Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                          Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                          Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                          Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                          Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                          Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                          Vehicle.Flags.LeftHandDrive))
          return;
        this.InvalidPath(vehicleID, ref data, vehicleID, ref data);
      }
      else
      {
        if ((int) building != (int) data.m_targetBuilding || (data.m_flags & Vehicle.Flags.GoingBack) !=
          ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
            Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
            Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
            Vehicle.Flags.Arriving | Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
            Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo |
            Vehicle.Flags.GoingBack | Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
            Vehicle.Flags.Exporting | Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
            Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
            Vehicle.Flags.Underground | Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
            Vehicle.Flags.LeftHandDrive))
          return;
        this.InvalidPath(vehicleID, ref data, vehicleID, ref data);
      }
    }

    private void RemoveSource(ushort vehicleID, ref Vehicle data)
    {
      if (data.m_sourceBuilding == (ushort) 0)
        return;
      Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) data.m_sourceBuilding]
        .RemoveOwnVehicle(vehicleID, ref data);
      data.m_sourceBuilding = (ushort) 0;
    }

    private void RemoveTarget(ushort vehicleID, ref Vehicle data)
    {
      if (data.m_targetBuilding == (ushort) 0)
        return;
      Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) data.m_targetBuilding]
        .RemoveGuestVehicle(vehicleID, ref data);
      data.m_targetBuilding = (ushort) 0;
    }

    public override void GetSize(ushort vehicleID, ref Vehicle data, out int size, out int max)
    {
      size = (int) data.m_transferSize;
      max = this.m_cargoCapacity;
    }

    public override void SimulationStep(ushort vehicleID, ref Vehicle data, Vector3 physicsLodRefPos)
    {
      if ((data.m_flags & Vehicle.Flags.WaitingCargo) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                           Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                           Vehicle.Flags.TransferToTarget |
                                                           Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                           Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                           Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                           Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                           Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                           Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                           Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                           Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                           Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                           Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                           Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                           Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                           Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                           Vehicle.Flags.LeftHandDrive))
      {
        // UnityEngine.Debug.LogWarning("waiting for cargo");
        bool flag = Singleton<SimulationManager>.instance.m_randomizer.Int32(2U) == 0;
        if (!flag && data.m_sourceBuilding != (ushort) 0 &&
            (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) data.m_sourceBuilding].m_flags &
             Building.Flags.Active) == Building.Flags.None)
          flag = true;
        if (!flag && data.m_targetBuilding != (ushort) 0 &&
            (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) data.m_targetBuilding].m_flags &
             Building.Flags.Active) == Building.Flags.None)
          flag = true;
        // UnityEngine.Debug.LogWarning($"flag: {flag}");
        if (!flag)
        {
          data.m_waitCounter = (int) data.m_transferSize < this.m_cargoCapacity
            ? (byte) Mathf.Min((int) data.m_waitCounter + 1, (int) byte.MaxValue)
            : byte.MaxValue;
          if (data.m_waitCounter == byte.MaxValue && ((data.m_flags & Vehicle.Flags.Spawned) !=
                                                      ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                        Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                        Vehicle.Flags.TransferToTarget |
                                                        Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                        Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                        Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                        Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                        Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                        Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                        Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                        Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                        Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                        Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                        Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                        Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                        Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                        Vehicle.Flags.LeftHandDrive) ||
                                                      this.CanSpawnAt(data.GetLastFramePosition())))
          {
            data.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned |
                            Vehicle.Flags.Inverted | Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource |
                            Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                            Vehicle.Flags.Stopped | Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                            Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                            Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace | Vehicle.Flags.GoingBack |
                            Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                            Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                            Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                            Vehicle.Flags.Underground | Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                            Vehicle.Flags.LeftHandDrive;
            data.m_flags |= Vehicle.Flags.Leaving;
            data.m_waitCounter = (byte) 0;
            UnityEngine.Debug.LogWarning("Start pathfinding");
            this.StartPathFind(vehicleID, ref data);
          }
        }
      }
      else if ((data.m_flags & Vehicle.Flags.Stopped) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                           Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                           Vehicle.Flags.TransferToTarget |
                                                           Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                           Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                           Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                           Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                           Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                           Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                           Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                           Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                           Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                           Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                           Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                           Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                           Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                           Vehicle.Flags.LeftHandDrive))
      {
        UnityEngine.Debug.LogWarning("stopped");
        if ((data.m_flags & Vehicle.Flags.Spawned) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                        Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                        Vehicle.Flags.TransferToTarget |
                                                        Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                        Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                        Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                        Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                        Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                        Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                        Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                        Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                        Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                        Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                        Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                        Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                        Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                        Vehicle.Flags.LeftHandDrive) &&
            ++data.m_waitCounter == (byte) 16)
        {
          UnityEngine.Debug.LogWarning("spawned");
          data.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned |
                          Vehicle.Flags.Inverted | Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource |
                          Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                          Vehicle.Flags.Leaving | Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                          Vehicle.Flags.TakingOff | Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                          Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                          Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                          Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                          Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                          Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding | Vehicle.Flags.LeftHandDrive;
          data.m_flags |= Vehicle.Flags.Leaving;
          data.m_waitCounter = (byte) 0;
        }
      }
      else if ((data.m_flags & Vehicle.Flags.GoingBack) == ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                             Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                             Vehicle.Flags.TransferToTarget |
                                                             Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                             Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                             Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                             Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                             Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                             Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                             Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                             Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                             Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                             Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                             Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                             Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                             Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                             Vehicle.Flags.LeftHandDrive) &&
               data.m_targetBuilding != (ushort) 0 &&
               (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) data.m_targetBuilding].m_flags &
                Building.Flags.Active) == Building.Flags.None)
        this.SetTarget(vehicleID, ref data, (ushort) 0);

      SimulationStep1(vehicleID, ref data, physicsLodRefPos);
    }

    public void SimulationStep1(ushort vehicleID, ref Vehicle data, Vector3 physicsLodRefPos)
    {
      if ((data.m_flags & Vehicle.Flags.WaitingPath) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                          Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                          Vehicle.Flags.TransferToTarget |
                                                          Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                          Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                          Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                          Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                          Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                          Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                          Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                          Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                          Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                          Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                          Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                          Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                          Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                          Vehicle.Flags.LeftHandDrive))
      {
        byte pathFindFlags = Singleton<PathManager>.instance.m_pathUnits.m_buffer[data.m_path].m_pathFindFlags;
        if (((int) pathFindFlags & 4) != 0)
        {
          data.m_pathPositionIndex = byte.MaxValue;
          data.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned |
                          Vehicle.Flags.Inverted | Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource |
                          Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 | Vehicle.Flags.Stopped |
                          Vehicle.Flags.Leaving | Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                          Vehicle.Flags.TakingOff | Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                          Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                          Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                          Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                          Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                          Vehicle.Flags.Underground | Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                          Vehicle.Flags.LeftHandDrive;
          data.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned |
                          Vehicle.Flags.Inverted | Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource |
                          Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                          Vehicle.Flags.Stopped | Vehicle.Flags.Leaving | Vehicle.Flags.Reversed |
                          Vehicle.Flags.TakingOff | Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                          Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                          Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                          Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                          Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                          Vehicle.Flags.Underground | Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                          Vehicle.Flags.LeftHandDrive;
          this.PathfindSuccess(vehicleID, ref data);
          this.TrySpawn(vehicleID, ref data);
        }
        else if (((int) pathFindFlags & 8) != 0)
        {
          data.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned |
                          Vehicle.Flags.Inverted | Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource |
                          Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 | Vehicle.Flags.Stopped |
                          Vehicle.Flags.Leaving | Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                          Vehicle.Flags.TakingOff | Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                          Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                          Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                          Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                          Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                          Vehicle.Flags.Underground | Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                          Vehicle.Flags.LeftHandDrive;
          Singleton<PathManager>.instance.ReleasePath(data.m_path);
          data.m_path = 0U;
          this.PathfindFailure(vehicleID, ref data);
          return;
        }
      }
      else if ((data.m_flags & Vehicle.Flags.WaitingSpace) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                Vehicle.Flags.TransferToTarget |
                                                                Vehicle.Flags.TransferToSource |
                                                                Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                                Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                                Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                                Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                                Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                Vehicle.Flags.WaitingSpace |
                                                                Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                                Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                                Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                                Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                                Vehicle.Flags.WaitingLoading |
                                                                Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                                                                Vehicle.Flags.Underground | Vehicle.Flags.Transition |
                                                                Vehicle.Flags.InsideBuilding |
                                                                Vehicle.Flags.LeftHandDrive))
        this.TrySpawn(vehicleID, ref data);

      Vector3 lastFramePosition = data.GetLastFramePosition();
      int lodPhysics = (double) Vector3.SqrMagnitude(physicsLodRefPos - lastFramePosition) < 1210000.0
        ? ((double) Vector3.SqrMagnitude(Singleton<SimulationManager>.instance.m_simulationView.m_position -
                                         lastFramePosition) < 250000.0
          ? 0
          : 1)
        : 2;
      this.SimulationStep(vehicleID, ref data, vehicleID, ref data, lodPhysics);
      if (data.m_leadingVehicle == (ushort) 0 && data.m_trailingVehicle != (ushort) 0)
      {
        VehicleManager instance = Singleton<VehicleManager>.instance;
        ushort vehicleID1 = data.m_trailingVehicle;
        int num = 0;
        while (vehicleID1 != (ushort) 0)
        {
          ushort trailingVehicle = instance.m_vehicles.m_buffer[(int) vehicleID1].m_trailingVehicle;
          instance.m_vehicles.m_buffer[(int) vehicleID1].Info.m_vehicleAI.SimulationStep(vehicleID1,
            ref instance.m_vehicles.m_buffer[(int) vehicleID1], vehicleID, ref data, lodPhysics);
          vehicleID1 = trailingVehicle;
          if (++num > 16384)
          {
            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + System.Environment.StackTrace);
            break;
          }
        }
      }

      int num1 = ItemClass.GetPrivateServiceIndex(this.m_info.m_class.m_service) == -1 ? 150 : 100;
      if (
        (data.m_flags & (Vehicle.Flags.Spawned | Vehicle.Flags.WaitingPath | Vehicle.Flags.WaitingSpace |
                         Vehicle.Flags.WaitingCargo)) == ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                           Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                           Vehicle.Flags.TransferToTarget |
                                                           Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                                           Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath |
                                                           Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                           Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                                           Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                           Vehicle.Flags.Landing | Vehicle.Flags.WaitingSpace |
                                                           Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                           Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                           Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                           Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                           Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion |
                                                           Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                                           Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                                           Vehicle.Flags.LeftHandDrive) &&
        data.m_cargoParent == (ushort) 0)
      {
        UnityEngine.Debug.LogWarning("Release 1");
        Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
      }
      else
      {
        if ((int) data.m_blockCounter != num1)
          return;
        UnityEngine.Debug.LogWarning("Release 2");
        Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
      }
    }

    private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
    {
      VehicleManager instance = Singleton<VehicleManager>.instance;
      ushort vehicleID1 = data.m_firstCargo;
      data.m_firstCargo = (ushort) 0;
      int num = 0;
      while (vehicleID1 != (ushort) 0)
      {
        ushort nextCargo = instance.m_vehicles.m_buffer[(int) vehicleID1].m_nextCargo;
        instance.m_vehicles.m_buffer[(int) vehicleID1].m_nextCargo = (ushort) 0;
        instance.m_vehicles.m_buffer[(int) vehicleID1].m_cargoParent = (ushort) 0;
        VehicleInfo info = instance.m_vehicles.m_buffer[(int) vehicleID1].Info;
        if (data.m_targetBuilding != (ushort) 0)
        {
          info.m_vehicleAI.SetSource(vehicleID1, ref instance.m_vehicles.m_buffer[(int) vehicleID1],
            data.m_targetBuilding);
          info.m_vehicleAI.SetTarget(vehicleID1, ref instance.m_vehicles.m_buffer[(int) vehicleID1],
            instance.m_vehicles.m_buffer[(int) vehicleID1].m_targetBuilding);
        }

        vehicleID1 = nextCargo;
        if (++num > 16384)
        {
          CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + System.Environment.StackTrace);
          break;
        }
      }

      data.m_waitCounter = (byte) 0;
      data.m_flags |= Vehicle.Flags.WaitingLoading;
      return false;
    }

    private bool ArriveAtSource(ushort vehicleID, ref Vehicle data)
    {
      VehicleManager instance = Singleton<VehicleManager>.instance;
      ushort vehicle = data.m_firstCargo;
      data.m_firstCargo = (ushort) 0;
      int num = 0;
      while (vehicle != (ushort) 0)
      {
        ushort nextCargo = instance.m_vehicles.m_buffer[(int) vehicle].m_nextCargo;
        instance.m_vehicles.m_buffer[(int) vehicle].m_nextCargo = (ushort) 0;
        instance.m_vehicles.m_buffer[(int) vehicle].m_cargoParent = (ushort) 0;
        UnityEngine.Debug.LogWarning("ArriveAtSource: release");
        instance.ReleaseVehicle(vehicle);
        vehicle = nextCargo;
        if (++num > 16384)
        {
          CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + System.Environment.StackTrace);
          break;
        }
      }

      data.m_waitCounter = (byte) 0;
      data.m_flags |= Vehicle.Flags.WaitingLoading;
      return false;
    }

    public override bool ArriveAtDestination(ushort vehicleID, ref Vehicle vehicleData)
    {
      if ((vehicleData.m_flags & Vehicle.Flags.WaitingTarget) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                   Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                   Vehicle.Flags.TransferToTarget |
                                                                   Vehicle.Flags.TransferToSource |
                                                                   Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                                   Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                                   Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                                   Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                                   Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                   Vehicle.Flags.WaitingSpace |
                                                                   Vehicle.Flags.WaitingCargo |
                                                                   Vehicle.Flags.GoingBack |
                                                                   Vehicle.Flags.WaitingTarget |
                                                                   Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                                                   Vehicle.Flags.Parking | Vehicle.Flags.CustomName |
                                                                   Vehicle.Flags.OnGravel |
                                                                   Vehicle.Flags.WaitingLoading |
                                                                   Vehicle.Flags.Congestion |
                                                                   Vehicle.Flags.DummyTraffic |
                                                                   Vehicle.Flags.Underground |
                                                                   Vehicle.Flags.Transition |
                                                                   Vehicle.Flags.InsideBuilding |
                                                                   Vehicle.Flags.LeftHandDrive))
        return false;
      if ((vehicleData.m_flags & Vehicle.Flags.WaitingLoading) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                    Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                    Vehicle.Flags.TransferToTarget |
                                                                    Vehicle.Flags.TransferToSource |
                                                                    Vehicle.Flags.Emergency1 |
                                                                    Vehicle.Flags.Emergency2 |
                                                                    Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                                    Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                                    Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                                    Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                    Vehicle.Flags.WaitingSpace |
                                                                    Vehicle.Flags.WaitingCargo |
                                                                    Vehicle.Flags.GoingBack |
                                                                    Vehicle.Flags.WaitingTarget |
                                                                    Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                                                    Vehicle.Flags.Parking | Vehicle.Flags.CustomName |
                                                                    Vehicle.Flags.OnGravel |
                                                                    Vehicle.Flags.WaitingLoading |
                                                                    Vehicle.Flags.Congestion |
                                                                    Vehicle.Flags.DummyTraffic |
                                                                    Vehicle.Flags.Underground |
                                                                    Vehicle.Flags.Transition |
                                                                    Vehicle.Flags.InsideBuilding |
                                                                    Vehicle.Flags.LeftHandDrive))
      {
        vehicleData.m_waitCounter = (byte) Mathf.Min((int) vehicleData.m_waitCounter + 1, (int) byte.MaxValue);
        if (vehicleData.m_waitCounter < (byte) 16)
          return false;
        if (vehicleData.m_targetBuilding != (ushort) 0 &&
            (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) vehicleData.m_targetBuilding].m_flags &
             Building.Flags.IncomingOutgoing) == Building.Flags.None)
        {
          ushort nextCargoParent = CargoTruckAI.FindNextCargoParent(vehicleData.m_targetBuilding,
            this.m_info.m_class.m_service, this.m_info.m_class.m_subService);
          if (nextCargoParent != (ushort) 0)
          {
            ushort targetBuilding = Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int) nextCargoParent]
              .m_targetBuilding;
            if (targetBuilding != (ushort) 0)
            {
              CargoTruckAI.SwitchCargoParent(nextCargoParent, vehicleID);
              vehicleData.m_waitCounter = (byte) 0;
              vehicleData.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned |
                                     Vehicle.Flags.Inverted | Vehicle.Flags.TransferToTarget |
                                     Vehicle.Flags.TransferToSource | Vehicle.Flags.Emergency1 |
                                     Vehicle.Flags.Emergency2 | Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                     Vehicle.Flags.Leaving | Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                                     Vehicle.Flags.TakingOff | Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                     Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                     Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                     Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                     Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic | Vehicle.Flags.Underground |
                                     Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                                     Vehicle.Flags.LeftHandDrive;
              this.SetTarget(vehicleID, ref vehicleData, targetBuilding);
              return (vehicleData.m_flags & Vehicle.Flags.Spawned) == ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                        Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                        Vehicle.Flags.TransferToTarget |
                                                                        Vehicle.Flags.TransferToSource |
                                                                        Vehicle.Flags.Emergency1 |
                                                                        Vehicle.Flags.Emergency2 |
                                                                        Vehicle.Flags.WaitingPath |
                                                                        Vehicle.Flags.Stopped | Vehicle.Flags.Leaving |
                                                                        Vehicle.Flags.Arriving |
                                                                        Vehicle.Flags.Reversed |
                                                                        Vehicle.Flags.TakingOff | Vehicle.Flags.Flying |
                                                                        Vehicle.Flags.Landing |
                                                                        Vehicle.Flags.WaitingSpace |
                                                                        Vehicle.Flags.WaitingCargo |
                                                                        Vehicle.Flags.GoingBack |
                                                                        Vehicle.Flags.WaitingTarget |
                                                                        Vehicle.Flags.Importing |
                                                                        Vehicle.Flags.Exporting |
                                                                        Vehicle.Flags.Parking |
                                                                        Vehicle.Flags.CustomName |
                                                                        Vehicle.Flags.OnGravel |
                                                                        Vehicle.Flags.WaitingLoading |
                                                                        Vehicle.Flags.Congestion |
                                                                        Vehicle.Flags.DummyTraffic |
                                                                        Vehicle.Flags.Underground |
                                                                        Vehicle.Flags.Transition |
                                                                        Vehicle.Flags.InsideBuilding |
                                                                        Vehicle.Flags.LeftHandDrive);
            }
          }
        }

        UnityEngine.Debug.LogWarning("ArriveAtDestination: release");
        Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
        return true;
      }

      return (vehicleData.m_flags & Vehicle.Flags.GoingBack) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                  Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                  Vehicle.Flags.TransferToTarget |
                                                                  Vehicle.Flags.TransferToSource |
                                                                  Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                                  Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                                  Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                                  Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                                  Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                  Vehicle.Flags.WaitingSpace |
                                                                  Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                                  Vehicle.Flags.WaitingTarget |
                                                                  Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                                                  Vehicle.Flags.Parking | Vehicle.Flags.CustomName |
                                                                  Vehicle.Flags.OnGravel |
                                                                  Vehicle.Flags.WaitingLoading |
                                                                  Vehicle.Flags.Congestion |
                                                                  Vehicle.Flags.DummyTraffic |
                                                                  Vehicle.Flags.Underground | Vehicle.Flags.Transition |
                                                                  Vehicle.Flags.InsideBuilding |
                                                                  Vehicle.Flags.LeftHandDrive)
        ? this.ArriveAtSource(vehicleID, ref vehicleData)
        : this.ArriveAtTarget(vehicleID, ref vehicleData);
    }

    public override void UpdateBuildingTargetPositions(
      ushort vehicleID,
      ref Vehicle vehicleData,
      Vector3 refPos,
      ushort leaderID,
      ref Vehicle leaderData,
      ref int index,
      float minSqrDistance)
    {
      Vector3 position;
      Vector3 target;
      if ((leaderData.m_flags & Vehicle.Flags.GoingBack) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                              Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                              Vehicle.Flags.TransferToTarget |
                                                              Vehicle.Flags.TransferToSource |
                                                              Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                              Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                              Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                              Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                              Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                              Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo |
                                                              Vehicle.Flags.GoingBack | Vehicle.Flags.WaitingTarget |
                                                              Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                                              Vehicle.Flags.Parking | Vehicle.Flags.CustomName |
                                                              Vehicle.Flags.OnGravel | Vehicle.Flags.WaitingLoading |
                                                              Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                                                              Vehicle.Flags.Underground | Vehicle.Flags.Transition |
                                                              Vehicle.Flags.InsideBuilding |
                                                              Vehicle.Flags.LeftHandDrive))
      {
        if (leaderData.m_sourceBuilding == (ushort) 0)
          return;
        BuildingManager instance = Singleton<BuildingManager>.instance;
        BuildingInfo info = instance.m_buildings.m_buffer[(int) leaderData.m_sourceBuilding].Info;
        Randomizer randomizer = new Randomizer((int) vehicleID);
        info.m_buildingAI.CalculateUnspawnPosition(leaderData.m_sourceBuilding,
          ref instance.m_buildings.m_buffer[(int) leaderData.m_sourceBuilding], ref randomizer, this.m_info,
          out position, out target);
        position = position * 2f - target;
      }
      else
      {
        if (vehicleData.m_targetBuilding == (ushort) 0)
          return;
        BuildingManager instance = Singleton<BuildingManager>.instance;
        BuildingInfo info = instance.m_buildings.m_buffer[(int) leaderData.m_targetBuilding].Info;
        Randomizer randomizer = new Randomizer((int) vehicleID);
        info.m_buildingAI.CalculateUnspawnPosition(leaderData.m_targetBuilding,
          ref instance.m_buildings.m_buffer[(int) leaderData.m_targetBuilding], ref randomizer, this.m_info,
          out position, out target);
        position = position * 2f - target;
      }

      Vector3 vector3_1 = (target + position) * 0.5f;
      Vector3 vector3_2 = (position - target).normalized * (this.m_info.m_generatedInfo.m_size.z * 0.5f);
      target = vector3_1 - vector3_2;
      position = vector3_1 + vector3_2;
      Vector4 vector4;
      vector4.x = target.x;
      vector4.y = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(target, false, 0.0f);
      vector4.z = target.z;
      vector4.w = 2f;
      ref Vehicle local1 = ref vehicleData;
      int num1;
      index = (num1 = index) + 1;
      int index1 = num1;
      Vector4 pos1 = vector4;
      local1.SetTargetPos(index1, pos1);
      if (index >= 4)
        return;
      vector4.x = position.x;
      vector4.y = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(position, false, 0.0f);
      vector4.z = position.z;
      vector4.w = 2f;
      ref Vehicle local2 = ref vehicleData;
      int num2;
      index = (num2 = index) + 1;
      int index2 = num2;
      Vector4 pos2 = vector4;
      local2.SetTargetPos(index2, pos2);
    }

    protected override bool StartPathFind(ushort vehicleID, ref Vehicle vehicleData)
    {
      UnityEngine.Debug.LogWarning("StartPathFind");
      if (vehicleData.m_leadingVehicle == (ushort) 0)
      {
        Vector3 startPos =
          (vehicleData.m_flags & Vehicle.Flags.Reversed) == ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                              Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                              Vehicle.Flags.TransferToTarget |
                                                              Vehicle.Flags.TransferToSource |
                                                              Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                              Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                              Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                              Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                              Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                              Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo |
                                                              Vehicle.Flags.GoingBack | Vehicle.Flags.WaitingTarget |
                                                              Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                                                              Vehicle.Flags.Parking | Vehicle.Flags.CustomName |
                                                              Vehicle.Flags.OnGravel | Vehicle.Flags.WaitingLoading |
                                                              Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                                                              Vehicle.Flags.Underground | Vehicle.Flags.Transition |
                                                              Vehicle.Flags.InsideBuilding |
                                                              Vehicle.Flags.LeftHandDrive)
            ? (Vector3) vehicleData.m_targetPos0
            : (Vector3) Singleton<VehicleManager>.instance.m_vehicles
              .m_buffer[(int) vehicleData.GetLastVehicle(vehicleID)].m_targetPos0;
        if ((vehicleData.m_flags & Vehicle.Flags.GoingBack) != ~(Vehicle.Flags.Created | Vehicle.Flags.Deleted |
                                                                 Vehicle.Flags.Spawned | Vehicle.Flags.Inverted |
                                                                 Vehicle.Flags.TransferToTarget |
                                                                 Vehicle.Flags.TransferToSource |
                                                                 Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 |
                                                                 Vehicle.Flags.WaitingPath | Vehicle.Flags.Stopped |
                                                                 Vehicle.Flags.Leaving | Vehicle.Flags.Arriving |
                                                                 Vehicle.Flags.Reversed | Vehicle.Flags.TakingOff |
                                                                 Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                                                                 Vehicle.Flags.WaitingSpace |
                                                                 Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                                                                 Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing |
                                                                 Vehicle.Flags.Exporting | Vehicle.Flags.Parking |
                                                                 Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                                                                 Vehicle.Flags.WaitingLoading |
                                                                 Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                                                                 Vehicle.Flags.Underground | Vehicle.Flags.Transition |
                                                                 Vehicle.Flags.InsideBuilding |
                                                                 Vehicle.Flags.LeftHandDrive))
        {
          if (vehicleData.m_sourceBuilding != (ushort) 0)
          {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance.m_buildings.m_buffer[(int) vehicleData.m_sourceBuilding].Info;
            Randomizer randomizer = new Randomizer((int) vehicleID);
            Vector3 position;
            info.m_buildingAI.CalculateSpawnPosition(vehicleData.m_sourceBuilding,
              ref instance.m_buildings.m_buffer[(int) vehicleData.m_sourceBuilding], ref randomizer, this.m_info,
              out position, out Vector3 _);
            var startPathFind = this.StartPathFind(vehicleID, ref vehicleData, startPos, position);
            UnityEngine.Debug.LogWarning($"Parent start pathfind to source: {startPathFind}");
            return startPathFind;
          }
        }
        else if (vehicleData.m_targetBuilding != (ushort) 0)
        {
          BuildingManager instance = Singleton<BuildingManager>.instance;
          BuildingInfo info = instance.m_buildings.m_buffer[(int) vehicleData.m_targetBuilding].Info;
          Randomizer randomizer = new Randomizer((int) vehicleID);
          Vector3 position;
          info.m_buildingAI.CalculateSpawnPosition(vehicleData.m_targetBuilding,
            ref instance.m_buildings.m_buffer[(int) vehicleData.m_targetBuilding], ref randomizer, this.m_info,
            out position, out Vector3 _);
          var startPathFind = this.StartPathFind(vehicleID, ref vehicleData, startPos, position);
          UnityEngine.Debug.LogWarning($"Parent start pathfind to target: {startPathFind}");
          return startPathFind;
        }
      }

      UnityEngine.Debug.LogWarning($"Own start pathfind: false");
      return false;
    }

    protected override bool StartPathFind(
      ushort vehicleID,
      ref Vehicle vehicleData,
      Vector3 startPos,
      Vector3 endPos)
    {
      if ((double) Vector3.SqrMagnitude(endPos - startPos) >= 100.0)
        return this.StartPathFind(vehicleID, ref vehicleData, startPos, endPos, true, true);
      UnityEngine.Debug.LogWarning($"sqr magnitude is less!");
      if (vehicleData.m_path != 0U)
      {
        Singleton<PathManager>.instance.ReleasePath(vehicleData.m_path);
        vehicleData.m_path = 0U;
      }

      vehicleData.m_flags &= Vehicle.Flags.Created | Vehicle.Flags.Deleted | Vehicle.Flags.Spawned |
                             Vehicle.Flags.Inverted | Vehicle.Flags.TransferToTarget | Vehicle.Flags.TransferToSource |
                             Vehicle.Flags.Emergency1 | Vehicle.Flags.Emergency2 | Vehicle.Flags.Stopped |
                             Vehicle.Flags.Leaving | Vehicle.Flags.Arriving | Vehicle.Flags.Reversed |
                             Vehicle.Flags.TakingOff | Vehicle.Flags.Flying | Vehicle.Flags.Landing |
                             Vehicle.Flags.WaitingSpace | Vehicle.Flags.WaitingCargo | Vehicle.Flags.GoingBack |
                             Vehicle.Flags.WaitingTarget | Vehicle.Flags.Importing | Vehicle.Flags.Exporting |
                             Vehicle.Flags.Parking | Vehicle.Flags.CustomName | Vehicle.Flags.OnGravel |
                             Vehicle.Flags.WaitingLoading | Vehicle.Flags.Congestion | Vehicle.Flags.DummyTraffic |
                             Vehicle.Flags.Underground | Vehicle.Flags.Transition | Vehicle.Flags.InsideBuilding |
                             Vehicle.Flags.LeftHandDrive;
      vehicleData.m_targetPos0 = (Vector4) endPos;
      vehicleData.m_targetPos0.w = 2f;
      vehicleData.m_targetPos1 = vehicleData.m_targetPos0;
      vehicleData.m_targetPos2 = vehicleData.m_targetPos0;
      vehicleData.m_targetPos3 = vehicleData.m_targetPos0;
      this.TrySpawn(vehicleID, ref vehicleData);
      return true;
    }

    protected virtual bool StartPathFind(
      ushort vehicleID,
      ref Vehicle vehicleData,
      Vector3 startPos,
      Vector3 endPos,
      bool startBothWays,
      bool endBothWays)
    {
      VehicleInfo info = this.m_info;
      PathUnit.Position pathPosA1;
      PathUnit.Position pathPosB1;
      float distanceSqrA1;
      PathUnit.Position pathPosA2;
      PathUnit.Position pathPosB2;
      float distanceSqrA2;
      var startPosFound = FindPathPosition(startPos, ItemClass.Service.PublicTransport, NetInfo.LaneType.Vehicle,
        info.m_vehicleType, false, false, 64f, out pathPosA1, out pathPosB1, out distanceSqrA1, out float _);
      var endPosFound = FindPathPosition(endPos, ItemClass.Service.PublicTransport, NetInfo.LaneType.Vehicle,
        info.m_vehicleType, false, false, 64f, out pathPosA2, out pathPosB2, out distanceSqrA2, out float _);
      UnityEngine.Debug.LogWarning($"StartPathFind-  startPosFound: {startPosFound}, endPosFound: {endPosFound}");
      if (startPosFound && endPosFound)
      {
        if (!startBothWays || (double) distanceSqrA1 < 10.0)
          pathPosB1 = new PathUnit.Position();
        if (!endBothWays || (double) distanceSqrA2 < 10.0)
          pathPosB2 = new PathUnit.Position();
        uint unit;
        if (Singleton<PathManager>.instance.CreatePath(out unit, ref Singleton<SimulationManager>.instance.m_randomizer,
          Singleton<SimulationManager>.instance.m_currentBuildIndex, pathPosA1, pathPosB1, pathPosA2, pathPosB2,
          NetInfo.LaneType.Vehicle, info.m_vehicleType, 20000f))
        {
          if (vehicleData.m_path != 0U)
            Singleton<PathManager>.instance.ReleasePath(vehicleData.m_path);
          vehicleData.m_path = unit;
          vehicleData.m_flags |= Vehicle.Flags.WaitingPath;
          return true;
        }
      }

      PathUnit.Position pathPosA3;
      PathUnit.Position pathPosB3;
      float distanceSqrA3;
      PathUnit.Position pathPosA4;
      PathUnit.Position pathPosB4;
      float distanceSqrA4;
      if (FindPathPosition(startPos, ItemClass.Service.Beautification, NetInfo.LaneType.Vehicle,
            info.m_vehicleType, false, false, 64f, out pathPosA3, out pathPosB3, out distanceSqrA3, out _) &&
          FindPathPosition(endPos, ItemClass.Service.Beautification, NetInfo.LaneType.Vehicle,
            info.m_vehicleType, false, false, 64f, out pathPosA4, out pathPosB4, out distanceSqrA4, out _))
      {
        if (!startBothWays || (double) distanceSqrA3 < 10.0)
          pathPosB3 = new PathUnit.Position();
        if (!endBothWays || (double) distanceSqrA4 < 10.0)
          pathPosB4 = new PathUnit.Position();
        uint unit;
        if (Singleton<PathManager>.instance.CreatePath(out unit, ref Singleton<SimulationManager>.instance.m_randomizer,
          Singleton<SimulationManager>.instance.m_currentBuildIndex, pathPosA3, pathPosB3, pathPosA4, pathPosB4,
          NetInfo.LaneType.Vehicle, info.m_vehicleType, 20000f))
        {
          if (vehicleData.m_path != 0U)
            Singleton<PathManager>.instance.ReleasePath(vehicleData.m_path);
          vehicleData.m_path = unit;
          vehicleData.m_flags |= Vehicle.Flags.WaitingPath;
          return true;
        }

      }

      return false;
    }
    
      public static bool FindPathPosition(
    Vector3 position,
    ItemClass.Service service,
    NetInfo.LaneType laneType,
    VehicleInfo.VehicleType vehicleType,
    bool allowUnderground,
    bool requireConnect,
    float maxDistance,
    out PathUnit.Position pathPosA,
    out PathUnit.Position pathPosB,
    out float distanceSqrA,
    out float distanceSqrB)
  {
    return FindPathPosition(position, service, service, laneType, vehicleType, VehicleInfo.VehicleType.None, allowUnderground, requireConnect, maxDistance, out pathPosA, out pathPosB, out distanceSqrA, out distanceSqrB);
  }

  public static bool FindPathPosition(
    Vector3 position,
    ItemClass.Service service,
    ItemClass.SubService subService,
    ItemClass.Level level,
    NetInfo.LaneType laneType,
    VehicleInfo.VehicleType vehicleType,
    bool allowUnderground,
    bool requireConnect,
    float maxDistance,
    out PathUnit.Position pathPosA,
    out PathUnit.Position pathPosB,
    out float distanceSqrA,
    out float distanceSqrB)
  {
    return FindPathPosition(position, ItemClass.Service.None, ItemClass.Service.None, service, subService, level, laneType, vehicleType, VehicleInfo.VehicleType.None, allowUnderground, requireConnect, maxDistance, out pathPosA, out pathPosB, out distanceSqrA, out distanceSqrB);
  }


  public static bool FindPathPosition(
    Vector3 position,
    ItemClass.Service service,
    ItemClass.Service service2,
    NetInfo.LaneType laneType,
    VehicleInfo.VehicleType vehicleType,
    VehicleInfo.VehicleType stopType,
    bool allowUnderground,
    bool requireConnect,
    float maxDistance,
    out PathUnit.Position pathPosA,
    out PathUnit.Position pathPosB,
    out float distanceSqrA,
    out float distanceSqrB)
  {
    return FindPathPosition(position, service, service2, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None, laneType, vehicleType, stopType, allowUnderground, requireConnect, maxDistance, out pathPosA, out pathPosB, out distanceSqrA, out distanceSqrB);
  }

  public static bool FindPathPosition(
    Vector3 position,
    ItemClass.Service service,
    ItemClass.Service service2,
    ItemClass.Service service3,
    ItemClass.SubService subService3,
    ItemClass.Level level3,
    NetInfo.LaneType laneType,
    VehicleInfo.VehicleType vehicleType,
    VehicleInfo.VehicleType stopType,
    bool allowUnderground,
    bool requireConnect,
    float maxDistance,
    out PathUnit.Position pathPosA,
    out PathUnit.Position pathPosB,
    out float distanceSqrA,
    out float distanceSqrB)
  {
    Bounds bounds = new Bounds(position, new Vector3(maxDistance * 2f, maxDistance * 2f, maxDistance * 2f));
    int num1 = Mathf.Max((int) (((double) bounds.min.x - 64.0) / 64.0 + 135.0), 0);
    int num2 = Mathf.Max((int) (((double) bounds.min.z - 64.0) / 64.0 + 135.0), 0);
    int num3 = Mathf.Min((int) (((double) bounds.max.x + 64.0) / 64.0 + 135.0), 269);
    int num4 = Mathf.Min((int) (((double) bounds.max.z + 64.0) / 64.0 + 135.0), 269);
    NetManager instance = Singleton<NetManager>.instance;
    pathPosA.m_segment = (ushort) 0;
    pathPosA.m_lane = (byte) 0;
    pathPosA.m_offset = (byte) 0;
    distanceSqrA = 1E+10f;
    pathPosB.m_segment = (ushort) 0;
    pathPosB.m_lane = (byte) 0;
    pathPosB.m_offset = (byte) 0;
    distanceSqrB = 1E+10f;
    float num5 = maxDistance * maxDistance;
    for (int index1 = num2; index1 <= num4; ++index1)
    {
      for (int index2 = num1; index2 <= num3; ++index2)
      {
        ushort nextGridSegment = instance.m_segmentGrid[index1 * 270 + index2];
        int num6 = 0;
        while (nextGridSegment != (ushort) 0)
        {
          NetInfo info = instance.m_segments.m_buffer[(int) nextGridSegment].Info;
          UnityEngine.Debug.LogWarning($"Trying segment {nextGridSegment} of type {info?.name}");
          if (info != null && (info.m_class.m_service == service || info.m_class.m_service == service2 || info.m_class.m_service == service3 && info.m_class.m_subService == subService3 && info.m_class.m_level == level3) && ((instance.m_segments.m_buffer[(int) nextGridSegment].m_flags & (NetSegment.Flags.Collapsed | NetSegment.Flags.Flooded)) == NetSegment.Flags.None && (allowUnderground || !info.m_netAI.IsUnderground())))
          {
            UnityEngine.Debug.LogWarning($"Service works!");
            ushort startNode = instance.m_segments.m_buffer[(int) nextGridSegment].m_startNode;
            ushort endNode = instance.m_segments.m_buffer[(int) nextGridSegment].m_endNode;
            Vector3 position1 = instance.m_nodes.m_buffer[(int) startNode].m_position;
            Vector3 position2 = instance.m_nodes.m_buffer[(int) endNode].m_position;
            float num7 = Mathf.Max(Mathf.Max(bounds.min.x - 64f - position1.x, bounds.min.z - 64f - position1.z), Mathf.Max((float) ((double) position1.x - (double) bounds.max.x - 64.0), (float) ((double) position1.z - (double) bounds.max.z - 64.0)));
            float num8 = Mathf.Max(Mathf.Max(bounds.min.x - 64f - position2.x, bounds.min.z - 64f - position2.z), Mathf.Max((float) ((double) position2.x - (double) bounds.max.x - 64.0), (float) ((double) position2.z - (double) bounds.max.z - 64.0)));
            Vector3 positionA;
            int laneIndexA;
            float laneOffsetA;
            Vector3 positionB;
            int laneIndexB;
            float laneOffsetB;
            if (((double) num7 < 0.0 || (double) num8 < 0.0) && (instance.m_segments.m_buffer[(int) nextGridSegment].m_bounds.Intersects(bounds) && instance.m_segments.m_buffer[(int) nextGridSegment].GetClosestLanePosition(position, laneType, vehicleType, stopType, requireConnect, out positionA, out laneIndexA, out laneOffsetA, out positionB, out laneIndexB, out laneOffsetB)))
            {
              UnityEngine.Debug.LogWarning($"A");
              float num9 = Vector3.SqrMagnitude(position - positionA);
              UnityEngine.Debug.LogWarning($"Evaluating position - position:{position},positionA: {positionA}, num5:{num5}, num9: {num9}");
              if ((double) num9 < (double) num5)
              {
                UnityEngine.Debug.LogWarning($"B");
                num5 = num9;
                pathPosA.m_segment = nextGridSegment;
                pathPosA.m_lane = (byte) laneIndexA;
                pathPosA.m_offset = (byte) Mathf.Clamp(Mathf.RoundToInt(laneOffsetA * (float) byte.MaxValue), 0, (int) byte.MaxValue);
                distanceSqrA = num9;
                float num10 = Vector3.SqrMagnitude(position - positionB);
                if (laneIndexB == -1 || (double) num10 >= (double) maxDistance * (double) maxDistance)
                {
                  UnityEngine.Debug.LogWarning($"C");
                  pathPosB.m_segment = (ushort) 0;
                  pathPosB.m_lane = (byte) 0;
                  pathPosB.m_offset = (byte) 0;
                  distanceSqrB = 1E+10f;
                }
                else
                {
                  UnityEngine.Debug.LogWarning($"D");
                  pathPosB.m_segment = nextGridSegment;
                  pathPosB.m_lane = (byte) laneIndexB;
                  pathPosB.m_offset = (byte) Mathf.Clamp(Mathf.RoundToInt(laneOffsetB * (float) byte.MaxValue), 0, (int) byte.MaxValue);
                  distanceSqrB = num10;
                }
              }
            }
          }
          nextGridSegment = instance.m_segments.m_buffer[(int) nextGridSegment].m_nextGridSegment;
          if (++num6 >= 36864)
          {
            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + System.Environment.StackTrace);
            break;
          }
        }
      }
    }
    return pathPosA.m_segment != (ushort) 0;
  }
  }
}
    
