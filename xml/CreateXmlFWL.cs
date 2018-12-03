using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


public class CreateXmlFWL
{
    public CreateXmlFWL() { }
    /// <summary>
    /// 生成xml文件
    /// </summary>
    /// <param name="fileFullPathName">xml文件的文件路径及文件名</param>
    public static void CreateFile(FWLXMLEntity xmlEntity, string fileFullPathName)
    {
        if (xmlEntity == null)
        {
            throw new Exception("空数据");
        }
        XmlDocument document = new XmlDocument();
        document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", ""));
        XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
        XmlElement product = document.CreateElement("product");

        document.AppendChild(product);

        product.AppendChild(newXmlElement(document, "version_number", xmlEntity.version_number));//版本号
        product.AppendChild(newXmlElement(document, "post_org", xmlEntity.post_org));//发布单位
        product.AppendChild(newXmlElement(document, "forecast_type", xmlEntity.forecast_type));//预报类型
        product.AppendChild(newXmlElement(document, "product_type", xmlEntity.product_type));//产品类型
        product.AppendChild(newXmlElement(document, "post_time", xmlEntity.post_time));//发布时间
        product.AppendChild(newXmlElement(document, "area", xmlEntity.area));//预报区域
        product.AppendChild(newXmlElement(document, "predict_aging", xmlEntity.predict_aging));//预报时效  字段名：
        product.AppendChild(newXmlElement(document, "element", xmlEntity.element));//预报要素

        XmlElement entities = document.CreateElement("entities");
        foreach (FWLEntity en in xmlEntity.entitys)
        {
            XmlElement entity = newXmlElement(document, "entity");
            entity.AppendChild(newXmlElement(document, "site", en.site));
            entity.AppendChild(newXmlElement(document, "name", en.name));
            entity.AppendChild(newXmlElement(document, "datum_plane", en.datum_plane));
            entity.AppendChild(newXmlElement(document, "hour0", en.hour0));
            entity.AppendChild(newXmlElement(document, "hour1", en.hour1));
            entity.AppendChild(newXmlElement(document, "hour2", en.hour2));
            entity.AppendChild(newXmlElement(document, "hour3", en.hour3));
            entity.AppendChild(newXmlElement(document, "hour4", en.hour4));
            entity.AppendChild(newXmlElement(document, "hour5", en.hour5));
            entity.AppendChild(newXmlElement(document, "hour6", en.hour6));
            entity.AppendChild(newXmlElement(document, "hour7", en.hour7));
            entity.AppendChild(newXmlElement(document, "hour8", en.hour8));
            entity.AppendChild(newXmlElement(document, "hour9", en.hour9));
            entity.AppendChild(newXmlElement(document, "hour10", en.hour10));
            entity.AppendChild(newXmlElement(document, "hour11", en.hour11));
            entity.AppendChild(newXmlElement(document, "hour12", en.hour12));
            entity.AppendChild(newXmlElement(document, "hour13", en.hour13));
            entity.AppendChild(newXmlElement(document, "hour14", en.hour14));
            entity.AppendChild(newXmlElement(document, "hour15", en.hour15));
            entity.AppendChild(newXmlElement(document, "hour16", en.hour16));
            entity.AppendChild(newXmlElement(document, "hour17", en.hour17));
            entity.AppendChild(newXmlElement(document, "hour18", en.hour18));
            entity.AppendChild(newXmlElement(document, "hour19", en.hour19));
            entity.AppendChild(newXmlElement(document, "hour20", en.hour20));
            entity.AppendChild(newXmlElement(document, "hour21", en.hour21));
            entity.AppendChild(newXmlElement(document, "hour22", en.hour22));
            entity.AppendChild(newXmlElement(document, "hour23", en.hour23));
            XmlElement objects = newXmlElement(document, "objects");
            XmlElement object_1 = newXmlElement(document, "object");
            object_1.AppendChild(newXmlElement(document, "high_tide_time", en.high_tide_time_1));
            object_1.AppendChild(newXmlElement(document, "high_tide_value", en.high_tide_value_1));
            object_1.AppendChild(newXmlElement(document, "low_tide_time", en.low_tide_time_1));
            object_1.AppendChild(newXmlElement(document, "low_tide_value", en.low_tide_value_1));
            XmlElement object_2 = newXmlElement(document, "object");
            object_2.AppendChild(newXmlElement(document, "high_tide_time", en.high_tide_time_2));
            object_2.AppendChild(newXmlElement(document, "high_tide_value", en.high_tide_value_2));
            object_2.AppendChild(newXmlElement(document, "low_tide_time", en.low_tide_time_2));
            object_2.AppendChild(newXmlElement(document, "low_tide_value", en.low_tide_value_2));
            objects.AppendChild(object_1);
            objects.AppendChild(object_2);
            entity.AppendChild(objects);
            entities.AppendChild(entity);
        }
        product.AppendChild(entities);//entities 含有子标签
        product.AppendChild(newXmlElement(document, "linkman", xmlEntity.linkman));//联系人 
        product.AppendChild(newXmlElement(document, "tel", xmlEntity.tel));//联系人 
        document.Save(fileFullPathName);
    }


    private static XmlElement newXmlElement(XmlDocument docu, string name, string text)
    {
        XmlElement ele = docu.CreateElement(name);
        if (text == null)
            text = "";
        ele.InnerText = text;
        return ele;
    }

    private static XmlElement newXmlElement(XmlDocument docu, string name)
    {
        XmlElement ele = docu.CreateElement(name);
        return ele;
    }
}
