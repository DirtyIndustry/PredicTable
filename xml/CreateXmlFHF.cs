using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


public class CreateXmlFHF
{
    public CreateXmlFHF()
    { }
    public static void CreateFile(FHFXMLEntity xmlEntity, string fileFullPathName)
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
        product.AppendChild(newXmlElement(document, "predict_aging", xmlEntity.predict_aging));//预报时效  
        product.AppendChild(newXmlElement(document, "element", xmlEntity.element));//预报要素

        XmlElement entities = document.CreateElement("entities");
        foreach (FHFEntity en in xmlEntity.entitys)
        {
            XmlElement entity = newXmlElement(document, "entity");
            entity.AppendChild(newXmlElement(document, "site", en.site));
            entity.AppendChild(newXmlElement(document, "name", en.name));
            entity.AppendChild(newXmlElement(document, "wind_speed_initial_value_from", en.wind_speed_initial_value_from));
            entity.AppendChild(newXmlElement(document, "wind_speed_initial_value_to", en.wind_speed_initial_value_to));
            entity.AppendChild(newXmlElement(document, "wind_speed_word", en.wind_speed_word));
            entity.AppendChild(newXmlElement(document, "wind_speed_change_value_from", en.wind_speed_change_value_from));
            entity.AppendChild(newXmlElement(document, "wind_speed_change_value_to", en.wind_speed_change_value_to));
            entity.AppendChild(newXmlElement(document, "wind_dir_from", en.wind_dir_from));
            entity.AppendChild(newXmlElement(document, "wind_dir_to", en.wind_dir_to));
            entity.AppendChild(newXmlElement(document, "wind_power", en.wind_power));
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
