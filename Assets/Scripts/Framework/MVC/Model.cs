

public abstract class Model 
{
    // 模型名称
    public abstract string Name { get; }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    protected void SendEvent(string eventName, object data =null) {
        MVC.SendEvent(eventName,data);
    }
}
