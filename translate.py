from flask import Flask, jsonify, request
import argostranslate.package
import argostranslate.translate

from pathlib import Path  # Импортируем Path

app = Flask(__name__)

@app.route('/api/translate', methods=['POST'])
def translate_text():
    data = request.get_json()
    text_to_translate = data.get('text')
    from_code_get = data.get('from_lan')
    to_code_get =  data.get('to_lan')
    print(text_to_translate)
    from_code = from_code_get
    to_code= to_code_get
    argostranslate.package.update_package_index()
    #Загружаем модели для перевода
    model = Path("translate-ru_en-1_9.argosmodel")
    argostranslate.package.install_from_path(model)
    #
    translateText= argostranslate.translate.translate(text_to_translate, from_code,to_code)
    # print(1)
    # argostranslate.package.update_package_index()
    # print(2)
    # available_packages = argostranslate.package.get_available_packages()
    # print(3)
    # package_to_install = next(
    #     filter(
    #         lambda x: x.from_code == from_code and x.to_code == to_code, available_packages
    #     )
    # )
    # print(4)
    # argostranslate.package.install_from_path(package_to_install.download())
    # print(5)
    # # Translate
    # translatedText = argostranslate.translate.translate(text_to_translate, from_code, to_code)


    return jsonify({"translated_text" : translateText})

if __name__ == '__main__':
  app.run(host='0.0.0.0', port=80)

# from_code = "ru"
# to_code="en"
# text_to_translate= input()
#
# model = Path("C:/Users/AV/PycharmProjects/django/translate_3.11/translate-ru_en-1_9.argosmodel")
# argostranslate.package.install_from_path(model)
#
# translateText= argostranslate.translate.translate(text_to_translate, from_code,to_code)
#
# print(translateText)
