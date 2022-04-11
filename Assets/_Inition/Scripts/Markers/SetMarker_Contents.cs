using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inition.HoneyWell.ParseData;

namespace Inition.Markers
{
    [System.Serializable]
    public class SetMarker_Contents : Base_SetMarker
    {
        [SerializeField]
        private string contentManagerName = "ContentPanel";

        public override void SetData(DataObject _obj, Transform _transform)
        {
            _transform.GetChildFromName<ContentManager>(contentManagerName).Initialize(_obj.contents.ToArray());
        }
    }
}