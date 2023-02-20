using System;
using UnityEngine;

namespace ProjectDiorama
{
    [CreateAssetMenu(fileName = "newConnectionType", menuName = "Scriptable Objects / ProjectDiorama / new ConnectionType")]
    public class ConnectionType : ScriptableObject
    {
        [SerializeField] TypeOfConnection _typeOfConnection;

        public bool IsCompatibleWith(ConnectionType connectionType)
        {
            return connectionType.TypeOfConnection switch
            {
                TypeOfConnection.None   => true,
                TypeOfConnection.Inlet  => _typeOfConnection == TypeOfConnection.Outlet,
                TypeOfConnection.Outlet => _typeOfConnection == TypeOfConnection.Inlet,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public TypeOfConnection TypeOfConnection => _typeOfConnection;
        public bool IsInlet => _typeOfConnection == TypeOfConnection.Inlet;

    }
}
