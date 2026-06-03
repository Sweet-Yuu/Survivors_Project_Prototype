using System.Collections.Generic;
using UnityEngine;

public interface IEventRewardStrategy
{
    bool CanExecute(PlayerStats player); // Kiểm tra xem người chơi có đủ điều kiện để nhận phần thưởng hay không
    void ExecuteReward(PlayerStats player); // Thực hiện việc trao phần thưởng cho người chơi
    
}



