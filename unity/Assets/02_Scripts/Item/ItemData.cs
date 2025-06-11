using System;

namespace _02_Scripts.Item
{
    public class ItemData
    {
        public int user_seq;
        public int slot_index;
        public string item_uid;
        public string item_name;
        public string item_description;
        public string item_icon;
        public int item_seq;
        public int quantity;
        public bool is_equipped;
        public DateTime? acquired_at;
        public DateTime? expired_at;
    }
}
