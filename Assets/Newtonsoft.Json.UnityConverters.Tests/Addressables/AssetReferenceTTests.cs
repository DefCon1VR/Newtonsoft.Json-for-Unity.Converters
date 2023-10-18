using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Newtonsoft.Json.UnityConverters.Tests.Addressables
{
    public class AssetReferenceTTests : TypeTester<AssetReferenceT<TextAsset>>
    {
        public static readonly IReadOnlyCollection<(AssetReferenceT<TextAsset> deserialized, object anonymous)> representations = new (AssetReferenceT<TextAsset>, object)[] {
            (new AssetReferenceT<TextAsset>("65ee4890b3c2b0f4db332c8305ce62bd"), "65ee4890b3c2b0f4db332c8305ce62bd"),
            (new AssetReferenceT<TextAsset>("31d1857eb02457d4d84fd43e6f32c401"), "31d1857eb02457d4d84fd43e6f32c401"),
            (new AssetReferenceT<TextAsset>("7446d4e9bc40aca48a68dc00da43f835"), "7446d4e9bc40aca48a68dc00da43f835"),
            (null, null),
        };

        protected override bool AreEqual([AllowNull] AssetReferenceT<TextAsset> a, [AllowNull] AssetReferenceT<TextAsset> b)
        {
            return a?.AssetGUID == b?.AssetGUID;
        }

        [Test]
        public void LoadsValidAssetTest()
        {
            var assetRef = Deserialize<AssetReferenceT<TextAsset>>("\"65ee4890b3c2b0f4db332c8305ce62bd\"");
            Assert.IsNotNull(assetRef);
            var textAsset = assetRef.LoadAssetAsync<TextAsset>().WaitForCompletion();
            Assert.AreEqual("Some sample text inside TextFile1.txt", textAsset.text.Trim());
        }

        [Test]
        public void SerializeEmptyGuidAsNull()
        {
            var assetRef = new AssetReferenceT<TextAsset>("");
            Assert.AreEqual("", assetRef.AssetGUID);
            string serialized = Serialize(assetRef);
            Assert.AreEqual("null", serialized);
        }
    }
}

