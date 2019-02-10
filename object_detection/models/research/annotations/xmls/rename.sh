for filename in *.xml; do
	ls $filename
	sed -i'*xml' -e 's/cone/cone/g' $filename
done
