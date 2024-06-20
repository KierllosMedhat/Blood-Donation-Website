
window.addEventListener('scroll', function() {
    const header = document.querySelector('header');
    const content = document.querySelector('.content');
    content.style.marginTop = header.offsetHeight + 'px';
});


    var cityProvinces = {
    "Cairo": ["Al-Qahira","Banha","Helwan" , "Madinat 15 May","Shubra El Kheima","Qalyub","Tukh","Al-Qanatir Al-Khayriyah","Al-Khanka",
        "Sixth of October","Al-Sheikh Zayed","Badr","New Cairo","New Heliopolis","Al-Obour","Al-Shorouk","Madinaty","New Administrative Capital"],
    "Alexandria": ["Abu Qir","Alexandria","Al-Ibrahimiyah","Al-Azarita","Al-Anfushi","Al-Hadrah","Al-Dekheila","Al-Saraya",
        "Al-Sayouf","Al-Shatby","Al-Agami","Al-Asafra","Al-Atareen","Al-Qabari","Al-Laban","Al-Maamoura","Al-Maks","Al-Montaza",
        "Al-Manshiyah","Al-Wardian","Bacos","Bahrī","Bolokly","Tharwat","Gleem","Gnanclis","Hay Al-Gomrok","Hay Al-Amiriya",
        "Hay Cleopatra","Hay Miami","Ras El Tin","Rushdi","Zizinia","Saba Pasha","San Stefano ","Sporting","Stanley","Smouha",
        "Sidi Bishr","Sidi Gaber","Shatby","Safar","Victoria ","Fleming","Kamb Shizar ","Karmouz","Kfar Abdou","Koum El-Deka",
        "Kont Zizinia","Loran","Mahram Bek","Mansheya El-Raml","Midan El-Manshiya"],
    "Giza": ["Minaşşat Al-Qanāţir","Awsim","Kerdasa","Abu Al-Nomros","Madina Al-Hawamidiya", "Al-Badrashin","Al-Ayat",
        "Al-Saff","Atfih","Al-Wahat Al-Bahriyah"],
    "Luxor":["Al-Zayniya","Al-Qurna","Al-Bayadia","Al-Tod","Armant","Esna"],
    "Aswan":[ "Edfu", "Drau", "Kom Ombo", "Nasr Al-Nuba"],
    "Sharm El Sheikh":["Naama Bay", "Hay Al-Hamidiya", "Hay Al-Nakhil", "Al-Hadiqa Al-Wataniya Ras Muhammad", "Wust Al-Medina", "Ras Nasrani"],
    "Hurghada": ["Ras Gharib", "Al-Qusair", "Safaga", "Marsa Alam", "Shalatein", "Hala'ib"],
    "Al-Gharbya": ["Kafr El-Zayat", "Al-Santa", "Al-Mahalla Al-Kubra", "Basyoun", "Zefta", "Samannoud", "Tanta ", "Qutur"],
    "Ismailia": ["Al-Tall Al-Kabeer", "Fayed", "Al-Qantara Sharq", "Al-Qantara Gharb", "Abu Sawir", "Al-Qasaseen"],
    "Port Said": ["Minaa Port Said", "Hay Al-Zuhour", "Hay Al-Sharq", "Hay Al-Dawahi", "Hay Al-Janoub", "Hay Al-Arab", "Hay Al-Shamal", "Hay Al-Menaakh", "Hay Al-Sharq Al-Sanai", "Hay Al-Iskan"],
    "Suez":["Minaa Al-Suweis (Port Said)", "Al-Ashir Min Ramadan", "Al-Suweis Al-Jadidah", "Al-Faysaliyah", "Al-Arab", "Hay Al-Janayin", "Hay Al-Arbaeen", "Hay Al-Sheikh Zayed", "Hay Al-Muntazah", "Hay Al-Salamat", "Hay Al-Janah", "Hay Al-Zuhour", "Hay Al-Nasr", "Hay Al-Junah", "Hay Al-Sharq", "Hay Al-Salam", "Hay Al-Masakin", "Hay Al-Amal", "Hay Al-Suweis Al-Qadimah"],
    "Fayoum":["Atsa", "Atsa Al-Sharqia", "Epsheeway", "Fayoum Al-Qadima", "Fayoum Al-Ramyseya", "Fayoum Al-Reda", "Fayoum Al-Senoris", "Fayoum El Gedida", "Tamya", "Youssef El-Sedeeq"],
    "Mansoura": ["Belqas", "Dakahlia", "El-Mansoura", "El-Mansoura El-Qadima", "El-Mansoura El-Jadida", "El-Mansoura El-Reda", "El-Mansoura El-Reda", "El-Senbelawein", "Mansoura"],
    "Al Sharqya":  ["Abu Hammad", "Abu Kabeer", "El-Ashir Muharram", "El-Ibrahimiya", "El-Qanayat", "El-Qareen", "El-Zagazig", "Mena Al-Qamh", "Minya Al-Qamh", "Sharqia New Capital"],
    "Beni Suef": ["Al-Fashn", "Bani Sweif", "Biba", "Naser", "New Bani Sweif", "Samasta"],
    "Minya": ["Abu Qirqas", "Al-Adwa", "Beni Mazar", "Deir Mawas", "Minya", "Minya Al-Qamh", "Minya El-Qadima", "Maghagha", "Mallawi", "Samalut", "Tallah"],
    "Damanhur": ["Damanhur Province 1", "Damanhur Province 2", "Damanhur Province 3"],
    "Qena": ["Qena Province 1", "Qena Province 2", "Qena Province 3"],
    "Banha": ["Banha Province 1", "Banha Province 2", "Banha Province 3"],
    "Kafr El Sheikh": ["Desouk", "Baltim", "Metoubes", "Qellin","Sidi Salem"],
    "Marsa Matruh": ["El Hamam", "El Dabaa", "Siwa", "Al-Alamein", "Sidi Barrani"],
    "Damietta":[ "El-Zarka", "Faraskur", "Kafr Saad", "New Damietta", "Ras El-Barr", "Zefta"],
    "Sinaa":["Al-Arish", "Bir al-Abed", "Nakhl", "Rafah"],
    "Asyut": ["Dirot", "Al-Qusiya", "Abnub","Abu Tig", "Al-Fateh", "Manfalout", "Al-Ghanaim","Sahel Salim", "Al-Badari", "MSidfa"],
    };

    function populateDropdown(selectId, optionsArray) {
    var dropdown = document.getElementById(selectId);

    optionsArray.sort();

    for (var i = 0; i < optionsArray.length; i++) {
        var option = document.createElement("option");
        option.value = optionsArray[i];
        option.text = optionsArray[i];
        dropdown.add(option);
    }
    }

    function populateProvinces() {
    var selectedCity = document.getElementById("egypt-cities").value;
    var provincesDropdown = document.getElementById("egypt-provinces");

    provincesDropdown.innerHTML = "";

    var provinces = cityProvinces[selectedCity] || [];
    populateDropdown("egypt-provinces", provinces);
    }

    var cities = Object.keys(cityProvinces);
    populateDropdown("egypt-cities", cities);

