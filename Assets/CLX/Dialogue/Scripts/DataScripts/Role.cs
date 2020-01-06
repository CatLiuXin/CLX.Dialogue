using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLX.Dialogue
{
    [CreateAssetMenu(menuName = "CLX/Dialogue/Create Role")]
    public class Role : ScriptableObject
    {
        public string roleName;
        public List<RoleImage> images;
    }
}
