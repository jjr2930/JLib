/*
 * 이 파일은 Factory의 사용법에 대한 내용이 담겨 있는 코드이다.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

namespace JLib.Core.FactoryMethodPattern
{
    
    public enum FurnitureType
    {
        Chair,
        Matress,
        Table,
        Wardrobe,
        Count
    }

    public class FurnitureCreationParameter : IFactoryCreationParameter
    {
        public FurnitureType type;
    }

    public interface IFurniture : IProduct
    {
    }

    public class Chair : IFurniture
    {
        public void SetMember(IFactoryCreationParameter creationParameter)
        {
            //nothing to do yet;
        }

        public void Use()
        {
            Debug.Log("Use Chair");
        }
    }

    public class Matress : IFurniture
    {
        public void SetMember(IFactoryCreationParameter creationParameter)
        {
            //nothing to do yet;
        }

        public void Use()
        {
            Debug.Log("Use Matress");
        }
    }

    public class Table : IFurniture
    {
        public void SetMember(IFactoryCreationParameter creationParameter)
        {
            //nothing to do
        }

        public void Use()
        {
            Debug.Log("Use Table");
        }
    }

    public class Wardrobe : IFurniture
    {
        public void SetMember(IFactoryCreationParameter creationParameter)
        {
            //nothing to do
        }

        public void Use()
        {
            Debug.Log("Use Wardrobe");
        }
    }

    public class FurnitureFactory : Factory<IFurniture>
    {
        public override IFurniture GetOne(IFactoryCreationParameter parameter)
        {
            FurnitureCreationParameter creationParameter = parameter as FurnitureCreationParameter;
            IFurniture newFurniture = null;
            switch (creationParameter.type)
            {
                case FurnitureType.Chair:
                    newFurniture = new Chair();
                    break;
                case FurnitureType.Matress:
                    newFurniture = new Matress();
                    break;
                case FurnitureType.Table:
                    newFurniture = new Table();
                    break;
                case FurnitureType.Wardrobe:
                    newFurniture = new Wardrobe();
                    break;
                default:
                    break;
            }

            newFurniture.SetMember(parameter);
            return newFurniture;
        }
    }

    public class Client : MonoBehaviour
    {
        public void Order()
        {
            var furnitureType = (FurnitureType)(UnityEngine.Random.Range(0, (int)FurnitureType.Count));
            FurnitureFactory factory = FindObjectOfType<FurnitureFactory>();
            if (null == factory)
                throw new NullReferenceException("there is no furniture factory..");

            var newFurniture = factory.GetOne(new FurnitureCreationParameter()
            {
                type = furnitureType
            });

            newFurniture.Use();
        }
    }
}
