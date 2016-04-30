using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI
{
    public class DataConversions
    {
        public static ResponseModels.DataPoint Convert(LazyObjects.PIPoint point)
        {
            ResponseModels.DataPoint result = new ResponseModels.DataPoint(point.ID, point.WebID, point.Name, point.Description, point.Path);
            result.PointClass = point.PointClass;
            result.PointType = point.PointType;
            result.Future = point.Future;

            return result;
        }

        public static ResponseModels.AFAttribute Convert(LazyObjects.AFAttribute Attrbute)
        {
            ResponseModels.AFAttribute result = new ResponseModels.AFAttribute(Attrbute.ID, Attrbute.WebID, Attrbute.Name, Attrbute.Description, Attrbute.Path);

            result.CategoryNames = Attrbute.Categories.ToList();
            result.ConfigString = Attrbute.ConfigString;
            result.DataReferencePlugIn = Attrbute.DataReferencePlugIn;
            result.Type = Attrbute.Type;

            return result;
        }

        public static ResponseModels.AFElement Convert(LazyObjects.AFElement element)
        {
            ResponseModels.AFElement result = new ResponseModels.AFElement(element.ID, element.WebID, element.Name, element.Description, element.Path);

            result.TemplateName = element.Template.Name;
            result.CategoryNames = element.Categories.ToList();

            return result;
        }

        public static ResponseModels.AFEventFrame Convert(LazyObjects.AFEventFrame frame)
        {
            ResponseModels.AFEventFrame result = new ResponseModels.AFEventFrame(frame.ID, frame.WebID, frame.Name, frame.Description, frame.Path);

            result.CategoryNames = frame.CategoryNames.ToList();
            result.StartTime = frame.StartTime;
            result.EndTime = frame.EndTime;
            result.TemplateName = frame.Template.Name;

            return result;
        }

        public static ResponseModels.AFDB Convert(LazyObjects.AFDatabase database)
        {
            ResponseModels.AFDB result = new ResponseModels.AFDB(database.ID, database.WebID, database.Name, database.Description, database.Path);

            return result;
        }

        public static ResponseModels.UnitClass Convert(LazyObjects.AFUnit unit)
        {
            ResponseModels.UnitClass result = new ResponseModels.UnitClass(unit.ID, unit.WebID, unit.Name, unit.Description, unit.Path);
            result.CanonicalUnitAbbreviation = unit.Abbreviation;

            return result;
        }
    }
}
