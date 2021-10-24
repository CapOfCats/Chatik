using System;
using System.Collections.Generic;
using System.Text;

namespace Nabrosok
{
    class Attachment
    {
        int ID;//Айди вложения
        byte type;//Тип(см.енум)++
        string name;//Название
        string src;//каво бля?
        string thumbnail;//айди миниатюры
        public Attachment(int id,byte typ,string nam, string sRc, string thumb)
        {
            this.ID = id;
            this.type = typ;
            this.name = nam;
            this.src = sRc;
            this.thumbnail = thumb;          
        }
        enum AttachmentType
        {
            Image=1
        }
       
    }
}
