using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerService.Interfaces
{
    interface IMongoService
    {
        //Generic implementations. Can be used for several tables
        public void InsertRecord<T>(string table, T record);

        public List<T> LoadRecords<T>(string table);

        public T LoadRecordByID<T>(string table, Guid id);

        public void UpsertRecord<T>(string table, Guid id, T record);

        public void DeleteRecord<T>(string table, Guid id);
    }
}
