# DataContractJsonSerializerDemo
```
Implementing serialization/deserialization with data contract json serializer
```

> In this repo, i m implementing 2 ways of serialization/deserialization with [data contract json serializer](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.json.datacontractjsonserializer) :
>
> :one: **BasicSerialization** 
>> Models are only decorated with [Serializable attribute](https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute)
>
> :two: **CustomSerialization**
>> Models are decorated with [Serializable attribute](https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute)
>>
>> Models are implementing [ISerializable interface](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)
>
> To run code in debug or release mode, type the following commands in your favorite terminal : 
> - `.\App.exe`
> - `.\App.exe Default`
> - `.\App.exe Basic`
> - `.\App.exe Custom`
>
>
> ![DataContractJsonSerializerDemoScreen](Screenshots/DataContractJsonSerializerDemoScreen.png)
>

**`Tools`** : vs19, net 5.0, data contract json serializer, bullseye
