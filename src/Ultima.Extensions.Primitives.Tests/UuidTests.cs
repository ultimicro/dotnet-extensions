namespace Ultima.Extensions.Primitives.Tests;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public sealed class UuidTests
{
    [TestMethod]
    public void FromGuid_WithNonEmptyGuid_ShouldReturnCorrespondingUuid()
    {
        var guid = new Guid("39b9b393-2a02-4945-b1e9-dd22e67f43cd");
        var uuid = Uuid.FromGuid(guid);

        Assert.AreEqual("39B9B3932A024945B1E9DD22E67F43CD", Convert.ToHexString(uuid));
    }

    [TestMethod]
    public void ToGuid_WithNonNilUuid_ShouldReturnCorrespondingGuid()
    {
        var uuid = Convert.FromHexString("39B9B3932A024945B1E9DD22E67F43CD");
        var guid = Uuid.ToGuid(uuid);

        Assert.AreEqual(new Guid("39b9b393-2a02-4945-b1e9-dd22e67f43cd"), guid);
    }
}
