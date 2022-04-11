using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inition.Draggables;

namespace Inition.Draggables.TechSpecs
{
    public class DraggableTechSpecsManager : Singleton<DraggableTechSpecsManager>
    {
        private List<DraggableTechSpecsController> techSpecs = new List<DraggableTechSpecsController>();

        public void AddDraggableTechSpecs(DraggableTechSpecsController _draggableTechSpecs)
        {
            techSpecs.Add(_draggableTechSpecs);
        }

        public void RemoveDraggablePhotos(DraggableTechSpecsController _draggableTechSpecs)
        {
            if (_draggableTechSpecs != null)
            {
                techSpecs.Remove(_draggableTechSpecs);
                Destroy(_draggableTechSpecs.gameObject);
            }
        }

        public bool DoesSpecExist(string _path)
        {
            foreach (DraggableTechSpecsController techSpec in techSpecs)
            {
                if (techSpec.GetPath() == _path)
                    return true;
            }
            return false;
        }

        public DraggableTechSpecsController GetSpec(string _path)
        {
            foreach (DraggableTechSpecsController techSpec in techSpecs)
            {
                if (techSpec.GetPath() == _path)
                    return techSpec;
            }
            return null;
        }
    }
}