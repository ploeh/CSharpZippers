namespace Ploeh.Samples.Zippers;

public sealed class FSZipperTests
{
    private static readonly FSItem myDisk =
        FSItem.CreateFolder("root",
        [
            FSItem.CreateFile("goat_yelling_like_man.wmv", "baaaaaa"),
            FSItem.CreateFile("pope_time.avi", "god bless"),
            FSItem.CreateFolder("pics",
            [
                FSItem.CreateFile("ape_throwing_up.jpg", "bleargh"),
                FSItem.CreateFile("watermelon_smash.gif", "smash!!"),
                FSItem.CreateFile("skull_man(scary).bmp", "Yikes!")
            ]),
            FSItem.CreateFile("dijon_poupon.doc", "best mustard"),
            FSItem.CreateFolder("programs",
            [
                FSItem.CreateFile("fartwizard.exe", "10gotofart"),
                FSItem.CreateFile("owl_bandit.dmg", "mov eax, h00t"),
                FSItem.CreateFile("not_a_virus.exe", "really not a virus"),
                FSItem.CreateFolder("source code",
                [
                    FSItem.CreateFile("best_hs_prog.hs", "main = print (fix error)"),
                    FSItem.CreateFile("random.hs", "main = print 4")
                ])
            ])
        ]);

    [Fact]
    public void GoToSkullMan()
    {
        var sut = new FSZipper(myDisk);

        var actual = sut.GoTo("pics")?.GoTo("skull_man(scary).bmp");

        Assert.NotNull(actual);
        Assert.Equal(
            FSItem.CreateFile("skull_man(scary).bmp", "Yikes!"),
            actual.FSItem);
    }
}
