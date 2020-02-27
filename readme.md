[TOC]

# CLX.Dialogue 插件使用说明文档

## 插件概括

### 插件目标

* 方便扩展新功能
* 尽量人性化、可视化的编辑文本操作
* 方便进行 Assetbundle 打包
* 简洁简单

### 插件预览

![目录结构](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E7%9B%AE%E5%BD%95%E7%BB%93%E6%9E%84.png>)

![编辑器](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E7%BC%96%E8%BE%91%E5%99%A8.png>)

![示例场景](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E7%A4%BA%E4%BE%8B%E5%9C%BA%E6%99%AF.gif>)

### 前话

​	在去年寒假期间曾经写过一个 [Super Dialogue](<https://github.com/CatLiuXin/Super-Dialogue>) 插件，但是存在有诸多的问题，综合起来大概有这些：

* 功能扩展麻烦

* 使用固定文件路径，不灵活

* 不便于热更新

  根据之前的经验，制作了当前的这款插件，虽然仍存在诸多问题，但是较之前者，相信还是会有比较大的进步。

### 新版本特性

#### v0.1.0

##### 说明

​	主要根据 v0.0.0 版本虽然可以使用文本携带文本信息，但是却很难对其他重要的文件信息进行处理保存的特性，开发了 v0.1.0 版本，其主要修改与新功能如下。

##### 值得注意的地方

* DialogueSetting处可以给特殊标记附加常规的文件类型枚举标记，表示该特殊标记会对应一个此种类型的文件（现支持音频、图片）。
* 使用Dialogue Editor时，如果勾选了具有非None的文件枚举的特殊标记的话，则会出现一个框用于选取资源。
* 可以通过DialogueClip类实例的GetDialogueObjectByMaskBit方法获取到相应资源。
* 如果感兴趣的话，可以打开Sample文件夹下的 `BGM与背景图` 场景进行了解。在这个场景中，使用本插件实现了点击按钮会出现一张背景图片与播放背景音乐的功能。

## 项目简易文档

### 总体设计说明图

#### 全图

![img](https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E6%80%BB%E4%BD%93%E8%AE%BE%E8%AE%A1.png)

#### 基础数据类说明图

![img](https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E5%9F%BA%E7%A1%80%E6%95%B0%E6%8D%AE%E7%B1%BB.png)

#### 编辑器设计说明图

![img](https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E7%BC%96%E8%BE%91%E5%99%A8%E8%AE%BE%E8%AE%A1.png)

#### 主要系统类/接口说明图

![img](https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E4%B8%BB%E8%A6%81%E7%B3%BB%E7%BB%9F%E7%B1%BB.png)

### 数据类型

​	数据类型是整个插件的基础，都处于 `CLX/Dialogue/Scripts/DataScripts` 文件夹内，主要包括下面三个类型：

* Role类用于角色的注册使用，其属性包括角色的姓名以及角色的各个表情描述与表情图片。

* Dialogue类用于包含对白种每个对白片段的所有数据，包括对白片段的内容，对白片段的角色以及对白的特殊标记。

* DialogueSetting类包含整个Dialogue系统的设置内容，其主要包括所有登场角色登记、对白特殊标记编辑注解以及若干设置。

  上面三个数据类型都采用了 `ScriptableObject` 类作为基类，便于编辑与使用AssetBundle打包。

#### Role类

```csharp
    /// 角色类 主要包括其名字与其主要图片
    [CreateAssetMenu(menuName = "CLX/Dialogue/Create Role")]
    public class Role : ScriptableObject
    {
        public string roleName;
        public List<RoleImage> images;
        /// 根据 Emotion 字符串来获取 RoleImage 对象，若找不到则返回 null
        public RoleImage GetImageByEmotion(string emotion)；
    }
    
    [System.Serializable]
    public class RoleImage
    {
        /// 表情图片
        public Sprite sprite;
        /// 表情描述
        public string emotion;
    }
```

​	Role类中两个数据成员分别记录角色的姓名与表情图片信息，RoleImage类用于记录每个表情的Sprite和表请描述。

#### Dialogue类

```csharp
	[CreateAssetMenu(menuName="CLX/Dialogue/Create Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<DialogueClip> dialogueClips;
    }
    [System.Serializable]
    public class DialogueClip
    {
        public string clipContext;
        public string roleName;
        public string roleEmotion;
        public int     eventMask;

        /// 是否包含指定的特殊标记位
        public bool HasMaskBit(int mask);

        /// 附加指定的特殊标记位
        public void AddMaskBit(int mask);

        /// 移除指定的特殊标记位
        public void RemoveMaskBit(int mask);

        /// 复制另一个clip的信息
        public void CopyBy(DialogueClip clip);
    }
```

​	Dialogue类中只包含一个DialogueClip的集合，DialogueClip类中包括了该片段的内容信息（包括片段的对白内容、说话的角色、角色的表情、特殊标记）以及与特殊标记位处理相关的一些方法。

#### DialogueSetting类

```csharp
	[CreateAssetMenu(menuName = "CLX/Dialogue/Create Dialogue Setting")]
    public class DialogueSetting : ScriptableObject
    {
    	/// 用于记录登场的所有角色
        public List<Role> roles;
        /// 特殊对白标记的提示名
        /// 编辑器使用时提示用户的作用
        public string[] maskNames;

        /// 编辑器上每行最多的mask数量
        [Range(1, 32)]
        public short maskColumeCount = 4;

        /// 根据roleName查找Role 若没找到则返回null
        public Role GetRoleByName(string roleName)；
    }
```

### 数据文件的使用

#### 数据文件的创建

![数据文件创建](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E6%95%B0%E6%8D%AE%E6%96%87%E4%BB%B6%E5%88%9B%E5%BB%BA.png>)





在Project视图下右击，Create->CLX->Dialogue->Create XXX 选取你想要创建的数据文件。

#### 各数据文件的内容

##### Role文件

![Role文件](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/Role%E6%96%87%E4%BB%B6.png>)

* 导入图片时必须要勾选Read/Write Enable

##### Setting文件

![img](https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/Setting%E6%96%87%E4%BB%B6.png)

##### Dialogue文件

![Dialogue文件.png](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/Dialogue%E6%96%87%E4%BB%B6.png>)

##### 值得说明的事情

​	以上三种文件，Role与Setting用户都可以根据自己的需求进行直接更改，而Dialogue文件请务必使用编辑器进行修改，否则请自行确保自己的角色、表情等信息无误。

### Dialogue编辑器的使用

#### Dialogue编辑器的外观

![编辑器](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/%E7%BC%96%E8%BE%91%E5%99%A8.png>)

#### 各编辑项

##### 对白设置

​	此处选取创建好的DialogueSetting文件，这是使用编辑器要做的第一步。

##### 对白

​	此处选取创建好的Dialogue文件，选择好后会进行一下验证，验证对白中出现的每个角色是否存在于对白设置文件中，若不存在则会对用户进行提示，若验证通过则弹出下面的编辑界面。

##### 角色名&表情

​	此处可以弹出下拉框选取，选取后左侧图片会更换为对应图片。

##### 对白内容

​	此处可以编辑对白片段的具体内容。

##### 应用修改&重置

​	应用修改则将编辑的信息写入文件，而点击重置的话会将返回最后一次应用修改时的内容。	

##### 上（下）一个片段

​	浏览上（下）一个对白片段，若片段的编号超出范围则会进行提示。

##### 加入（删除）片段

​	点击加入会在此片段的后添加一个新片段，然后会用默认的信息初始化。

​	点击删除会将片段删除，若删除的片段是对白中唯一的片段，会默认加入一个新的片段。

##### EventMask

​	此处显示Setting文件中的MaskNames，对应就是一个int数字的32个bit的位标记，在此处可以直接对其进行编辑。

### 核心类型

​	核心类型都位于 `CLX/Dialogue/Scripts/Core` 文件夹中，主要包括定义的一些接口以及比较重要的类。

#### DialogueMgr类

```csharp
	public class DialogueMgr : Singleton<DialogueMgr>
    {
        List<IDialogueMgrRegister> registers = new List<IDialogueMgrRegister>();
        Dictionary<string, Role> roles = new Dictionary<string, Role>();

        DialogueMgr() { }

        /// 注册一个新Role
        public void AddRole(Role role)；

        /// 若勾选removeAll则清空所有Role
        /// 否则将位于selectedRoles内的role取消注册
        public void RemoveRoles(bool removeAll=true,params Role[] selectedRoles)；

        /// 将选中role取消注册
        public void RemoveRole(Role role)；

        /// 根据name返回对应的Role 不存在则返回null
        public Role GetRoleByName(string name)；

        /// 注册一个IDialogueMgrRegister
        public void AddRegister(IDialogueMgrRegister helper)；

        /// 若勾选removeAll则清空所有Register
        /// 否则将位于selectedHelpers内的Register取消注册
        public void RemoveRegisters(bool removeAll=true,params IDialogueMgrRegister[] selectedRegisters)；

        /// 将选中Register取消注册
        public void RemoveRegister(IDialogueMgrRegister helper)；

        /// 开启一段对白
        public void DoDialogue(Dialogue dialogue)；

        /// 根据roleName和emotion找到对应的Sprite，找不到则返回null
        public Sprite GetSpriteByRoleEmotion(string roleName,string emotion);
    }
    

	public interface IDialogueMgrRegister
    {
        void OnDialogueStart(Dialogue dialogue);
    }
```

​	DialogueMgr采用单例模式，主要给外界提供一个对Role、IDialogueMgrRegister进行注册和对外界提供一个全局的开启对白事件的作用。

​	IDialogueMgrRegister中只有OnDialogueStart方法，注册的IDialogueMgrRegister会在调用DoDialogue方法的时候调用OnDialogueStart方法。

#### DialogueTrigger类

```csharp
    /// 用于对白触发
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        private Dialogue dialogue=null;

        public Dialogue Dialogue { get => dialogue; set => dialogue = value; }

        /// 开启一段对白
        public void BeginDialogue()
    }
```

​	这个类主要用于维护一个Dialogue，然后用于开启这个对白。

#### DialogueSamplePanel类

```csharp
    /// 主要用于注册Dialogue事件
    public class DialogueSamplePanel : MonoBehaviour, IDialogueMgrRegister
    {
		/// 字段内容对功能理解作用不大，故不予展示
		
		/// 因为DialogueSamplePanel类实现了IDialogueMgrRegister接口
		/// 这个方法会被注册到DialogueMgr实例去
		/// 会调用注册的IDilogueController的OnDialogueEnter方法
        public void OnDialogueStart(Dialogue dialogue)；
		
		/// 会调用注册的IDilogueController的OnDialogueClipEnter方法
        private void OnDialogueClipEnter(DialogueClip clip)；
        
		/// 会调用注册的IDilogueController的OnDialogueClipEnd方法
        private void OnDialogueClipEnd(DialogueClip clip)；

        /// 切换到clipCount个片段 若输入的片段号不合法 则触发结束对白事件
        public void ClipSwitchTo(int clipCount)；

        /// 切换到下一个对白片段，若在这之后没有对白片段了，则结束对白
        public void ShowNextClip()；
		
		/// 会调用注册的IDilogueController的OnDialogueEnd方法
        public void OnDialogueEnd()；

		/// 用于绑定IDialogueController
        public void BindController(IDialogueController controller)；
    }
    
	public interface IDialogueController
    {
        void OnDialogueEnter(Dialogue dialogue);
        void OnDialogueClipEnter(DialogueClip clip);
        void OnDialogueClipEnd(DialogueClip clip);
        void OnDialogueEnd(Dialogue dialogue);
    }
```

#### EventDialogueController类

​	下图是EventDialogueController的注册事件顺序，EventDialogueController实现了IDialogueController接口，但是我们要用它来处理特殊标记位的对白片段所以增加了OnSpecialDialogueEvent等内容。

![事件执行顺序](<https://raw.githubusercontent.com/CatLiuXin/Pics/master/CLX.Dialogue/DIalogue%E4%BA%8B%E4%BB%B6%E9%A1%BA%E5%BA%8F.png>)

```csharp
	public class EventDialogueController : MonoBehaviour, IDialogueController
    {
        public event Action<Dialogue> OnDialogueEnterEvent;
        public event Action<Dialogue> OnDialogueEndEvent;
        public event Action<DialogueClip> OnDialogueClipEnterEvent;
        public event Action<DialogueClip> OnDialogueClipEndEvent;
        
        public void OnDialogueClipEnd(DialogueClip clip);

        public void OnDialogueClipEnter(DialogueClip clip);

        public void OnDialogueEnd(Dialogue dialogue);

        public void OnDialogueEnter(Dialogue dialogue);

        /// 将action注册为mask所对应的Enter事件
        public void RegisterSpecialClipEnterAction(int mask,Action<DialogueClip> action);

        /// 将action注册为mask所对应的End事件
        public void RegisterSpecialClipEndAction(int mask, Action<DialogueClip> action);
		
		/// 切换到下一个片段
        public void ShowNextClip();
        
        /// 切换到clipCount个片段 若输入的片段号不合法 则触发结束对白事件
        public void ClipSwitchTo(int clipCount)
        {
            panel.ClipSwitchTo(clipCount);
        }
    }
```

### 示例

​	在CLX/Dialogue/Sample文件夹中有着演示的场景的所有资源与代码，各位可以打开进行学习。

​	示例中实现了一个简易的，分支语句功能，希望能够对各位有所启示。

### 值得注意的事情

1.  导入图片时必须要勾选Read/Write Enable。
2.  因为Role等脚本继承自ScriptableObject，ScriptableObject中有一个name属性，而往往我们不会使用这个属性而想使用比如roleName属性。
3. 如果Sprite进行了分割后，不能将其整体作为一个Sprite来使用了，使用的话只会显示分割的第一个图片。