using Planificador_Fabrica.Enums;

namespace Planificador_Fabrica.Filters
{
    public class Filter
    {
        public List<FilterCriteria> Filters {  get; private set; }

        public Filter() 
        {
            Filters = new List<FilterCriteria>();
        }

        public void Add(string fieldName, FilterCondition filterCondition, object value)
        {
            Filters.Add(new FilterCriteria
            {
                FieldName = fieldName,
                FilterCondition = filterCondition,
                Value = value
            });
        }
    }

    public class FilterCriteria
    {
        public string FieldName;
        public FilterCondition FilterCondition;
        public object Value;
    }
}
