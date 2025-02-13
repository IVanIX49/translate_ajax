import argostranslate.package
import argostranslate.translate

from_code = "en"
to_code = "ru"

# Download and install Argos Translate package
print(1)
argostranslate.package.update_package_index()
print(2)
available_packages = argostranslate.package.get_available_packages()
print(3)
package_to_install = next(
    filter(
        lambda x: x.from_code == from_code and x.to_code == to_code, available_packages
    )
)
print(4)
argostranslate.package.install_from_path(package_to_install.download())
print(5)
# Translate
translatedText = argostranslate.translate.translate("Hello mam", from_code, to_code)
print(translatedText)
# '¡Hola Mundo!'