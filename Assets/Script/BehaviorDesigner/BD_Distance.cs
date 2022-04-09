using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
    [TaskCategory("Basic/Transform")]
    [TaskDescription("Returns the distance between two Transforms.")]
    public class BD_Distance : Action
    {
        [Tooltip("The first Transform")]
        public SharedTransform firstTransform;
        [Tooltip("The second Transform")]
        public SharedTransform secondTransform;
        [Tooltip("The distance")]
        [RequiredField]
        public SharedFloat storeResult;

        public override TaskStatus OnUpdate()
        {
            storeResult.Value = Vector3.Distance(firstTransform.Value.position, secondTransform.Value.position);
            return TaskStatus.Running;
        }

        public override void OnReset()
        {
            //firstTransform = secondTransform = Vector3.zero;
            storeResult = 0;
        }
    }
}
