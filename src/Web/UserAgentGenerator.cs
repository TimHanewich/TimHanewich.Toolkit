using System;
using Newtonsoft.Json;

namespace TimHanewich.Toolkit.Web
{
    public class UserAgentGenerator
    {
        public static string RandomUserAgent()
        {
            //Assemble name part
            string NamePart = "";
            Random r = new Random();
            if (r.Next(0, 2) == 1)
            {
                NamePart = RandomWord() + RandomWord().ToUpper();
            }
            else
            {
                NamePart = RandomWord();
            }

            //Assemble version part
            string VersionPart = RandomNumber().ToString() + "." + RandomNumber().ToString() + "." + RandomNumber().ToString();

            string ToReturn = NamePart = "/" + VersionPart;
            return ToReturn;
        }

        private static string RandomWord()
        {
            string[] RandomWords = JsonConvert.DeserializeObject<string[]>("[\"lost\",\"rider\",\"rush\",\"charity\",\"constitution\",\"care\",\"braid\",\"effective\",\"conservative\",\"adjust\",\"nationalist\",\"pawn\",\"sock\",\"suspect\",\"boy\",\"packet\",\"eavesdrop\",\"candidate\",\"standard\",\"qualified\",\"reception\",\"aid\",\"blow\",\"hay\",\"twist\",\"glare\",\"jurisdiction\",\"organisation\",\"basic\",\"frank\",\"bitch\",\"cause\",\"mechanical\",\"drawing\",\"soldier\",\"subject\",\"idea\",\"embryo\",\"course\",\"use\",\"angel\",\"corn\",\"whole\",\"traffic\",\"basketball\",\"available\",\"catch\",\"fade\",\"intention\",\"ego\",\"shock\",\"redundancy\",\"relinquish\",\"roll\",\"hand\",\"cooperative\",\"absence\",\"long\",\"fragrant\",\"theme\",\"decisive\",\"star\",\"timetable\",\"dorm\",\"financial\",\"visual\",\"viable\",\"coal\",\"tease\",\"possibility\",\"jacket\",\"heroin\",\"parallel\",\"brilliance\",\"real\",\"square\",\"advance\",\"variety\",\"weakness\",\"huge\",\"layout\",\"dilemma\",\"deny\",\"mouse\",\"shaft\",\"motivation\",\"courage\",\"circumstance\",\"property\",\"dragon\",\"native\",\"dull\",\"ant\",\"movement\",\"fog\",\"concentration\",\"prey\",\"snap\",\"retired\",\"reaction\",\"exact\",\"west\",\"dinner\",\"outer\",\"slab\",\"curve\",\"shoulder\",\"contraction\",\"goat\",\"pat\",\"government\",\"deviation\",\"correspond\",\"owner\",\"program\",\"senior\",\"instruction\",\"negotiation\",\"subject\",\"country\",\"density\",\"ostracize\",\"hotdog\",\"elegant\",\"equal\",\"chop\",\"expand\",\"shower\",\"fluctuation\",\"experience\",\"implication\",\"prosper\",\"fitness\",\"delete\",\"suntan\",\"small\",\"offense\",\"cast\",\"open\",\"concentrate\",\"tactic\",\"heavy\",\"army\",\"am\",\"drink\",\"deter\",\"percent\",\"gallery\",\"volume\",\"beach\",\"progress\",\"north\",\"copy\",\"honest\",\"solo\",\"meat\",\"role\",\"prove\",\"sword\",\"conference\",\"horn\",\"reasonable\",\"hardware\",\"sausage\",\"lion\",\"wisecrack\",\"paper\",\"pace\",\"disagreement\",\"house\",\"lid\",\"city\",\"sex\",\"bow\",\"stroll\",\"neighborhood\",\"still\",\"expose\",\"voyage\",\"sit\",\"sphere\",\"he\",\"bind\",\"revolution\",\"youth\",\"misplace\",\"demonstration\",\"passive\",\"arch\",\"hemisphere\",\"incentive\",\"waste\",\"opposed\",\"braid\",\"therapist\",\"thirsty\",\"decrease\",\"colon\",\"suntan\",\"reproduction\",\"monstrous\",\"offer\",\"medium\",\"counter\",\"heal\",\"formal\",\"clue\",\"halt\",\"discrimination\",\"background\",\"ribbon\",\"swim\",\"flesh\",\"cord\",\"colleague\",\"audience\",\"behavior\",\"hold\",\"glimpse\",\"soak\",\"tone\",\"ambition\",\"landscape\",\"betray\",\"part\",\"invisible\",\"clean\",\"rich\",\"entertainment\",\"beach\",\"lose\",\"dine\",\"accountant\",\"inhibition\",\"win\",\"gene\",\"try\",\"bronze\",\"manufacturer\",\"timber\",\"bold\",\"shadow\",\"crutch\",\"friendly\",\"report\",\"constellation\",\"support\",\"stage\",\"world\",\"pollution\",\"inflation\",\"owl\",\"lead\",\"shark\",\"crowd\",\"distinct\",\"continuation\",\"dozen\",\"finger\",\"president\",\"announcement\",\"chase\",\"safety\",\"lung\",\"laboratory\",\"weed\",\"drawer\",\"cottage\",\"pawn\",\"marsh\",\"rocket\",\"turkey\",\"nonsense\",\"influence\",\"beneficiary\",\"environment\",\"kidney\",\"squeeze\",\"work\",\"disagree\",\"senior\",\"surprise\",\"organisation\",\"tiptoe\",\"redeem\",\"dialect\",\"judicial\",\"digital\",\"concept\",\"amuse\",\"middle\",\"wheat\",\"us\",\"notice\",\"raw\",\"intention\",\"snarl\",\"twin\",\"association\",\"notorious\",\"social\",\"moment\",\"leash\",\"convenience\",\"suffering\",\"superior\",\"contain\",\"wine\",\"evoke\",\"measure\",\"soft\",\"blame\",\"bake\",\"blast\",\"drug\",\"breast\",\"soprano\",\"seller\",\"burst\",\"bother\",\"energy\",\"freshman\",\"manage\",\"combination\",\"creation\",\"right\",\"profit\",\"perfect\",\"education\",\"deter\",\"sheet\",\"sister\",\"boot\",\"surprise\",\"park\",\"comfort\",\"way\",\"prevent\",\"decisive\",\"trade\",\"mechanism\",\"scheme\",\"business\",\"responsibility\",\"haircut\",\"plot\",\"instruction\",\"doctor\",\"decline\",\"ostracize\",\"love\",\"scan\",\"liver\",\"presence\",\"fabricate\",\"hypothesize\",\"coffin\",\"appetite\",\"medicine\",\"real\",\"wilderness\",\"fortune\",\"by\",\"bald\",\"prefer\",\"identity\",\"refund\",\"appoint\",\"attachment\",\"shock\",\"reach\",\"read\",\"dirty\",\"contribution\",\"resignation\",\"seize\",\"studio\",\"dull\",\"revival\",\"rabbit\",\"firefighter\",\"sleeve\",\"palm\",\"nuance\",\"inhabitant\",\"conservation\",\"telephone\",\"permission\",\"viable\",\"straight\",\"stadium\",\"wedding\",\"recommend\",\"helicopter\",\"encourage\",\"loop\",\"liberal\",\"slant\",\"resource\",\"fascinate\"]");
            Random r = new Random();
            return RandomWords[r.Next(0, RandomWords.Length-1)];
        }

        private static int RandomNumber()
        {
            Random r = new Random();
            int val = r.Next();
            int ToReturn = Convert.ToInt32(val.ToString().Substring(0, 1));
            return ToReturn;
        }
    }
}