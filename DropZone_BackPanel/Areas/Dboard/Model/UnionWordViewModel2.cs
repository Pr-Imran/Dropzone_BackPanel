using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace DropZone_BackPanel.Areas.Dboard.Model
{
    public class UnionWordViewModel2
    {
        public int Id { get; set; }
        public string thanaId { get; set; }
        public Thana thana { get; set; }
        public string? unionCode { get; set; }
        public string? unionName { get; set; }
        public string? unionNameBn { get; set; }
        public string? shortName { get; set; }
        public string? isActive { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }

        public IEnumerable<UnionWard>? unionWards { get; set; }
        public IEnumerable<Thana>? thanas { get; set; }
        public IEnumerable<Division>? divisions { get; set; }

    }
}
